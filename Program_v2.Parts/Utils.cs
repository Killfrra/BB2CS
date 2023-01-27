using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string Indent(this string str, int count = 4)
    {
        return Regex.Replace(str, @"^", "".PadRight(count), RegexOptions.Multiline);
    }
    public static string UCFirst(this string str)
    {
        return Char.ToUpperInvariant(str[0]) + str.Substring(1);
    }
    public static string LCFirst(this string str)
    {
        return Char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}

public static class Utils
{
    public static object? AskConstants(object? value)
    {
        if(value is string s && s.StartsWith("$") && s.EndsWith("$"))
        {
            s = s.Substring(1, s.Length - 1 - 1);
            var constant = Constants.Table.GetValueOrDefault(s);
            //if(constant == null)
            //    Console.WriteLine($"Failed to resolve constant {s}");
            return constant ?? value;
        }
        return value;
    }
    
    public static T? As<T>(this object? obj)
    {
        if(obj == null) return default;
        obj = AskConstants(obj);
        if(obj == null) return default;
        var typeTo = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        
        if(obj is double d && typeTo == typeof(float))
            return (T)((object)((float)d));
        if(obj is long l)
        {
            if(typeTo == typeof(int))
                return (T)((object)((int)l));
            if(typeTo == typeof(float))
                return (T)((object)((float)l));
        }

        var typeFrom = obj.GetType();
        if(obj.GetType().IsAssignableTo(typeTo))
            return (T)obj;
        Console.WriteLine($"{typeFrom} {obj} -> {typeTo}");
        //return (T)Convert.ChangeType(obj, typeTo);
        throw new ArgumentException();
    }

    public static bool? Invert(bool? src) => (src == null) ? null : !src;

    public static T[]? ReadArray<T>(this Dictionary<string, object> globals, HashSet<string> used, string name, T defaultValue)
    {
        var list = new List<T>();
        foreach(var (key, value) in globals)
        {
            int i = 1;
            if(key.StartsWith(name) &&
                (key.Length == name.Length || int.TryParse(key.Substring(name.Length), out i))
            ){
                Debug.Assert(i > 0);
                i--;
                while(list.Count <= i)
                    list.Add(defaultValue);
                list[i] = (T)value;
                used.Add(key);
            }
        }
        if(list.Count > 0)
            return list.ToArray();
        else
            return null;
    }

    public static string PrepareName(string name, bool ucfirst)
    {
        name = Regex.Replace(name, @"\W","_");
        if(Regex.IsMatch(name, @"^[^a-z_]|^true$|^false$", RegexOptions.IgnoreCase)) //TODO: Other keywords, just to be
        {
            name = "_" + name;
        }
        if(ucfirst)
            name = name.UCFirst();
        else
            name = name.LCFirst();
        return name;
    }

    static Dictionary<Type, string> primitiveTypes = new Dictionary<Type, string>
    {
        { typeof(bool), "bool" },
        { typeof(byte), "byte" },
        { typeof(char), "char" },
        { typeof(decimal), "decimal" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(int), "int" },
        { typeof(long), "long" },
        { typeof(sbyte), "sbyte" },
        { typeof(short), "short" },
        { typeof(string), "string" },
        { typeof(BBBuffName), "string" },
        { typeof(BBSpellName), "string" },
        { typeof(uint), "uint" },
        { typeof(ulong), "ulong" },
        { typeof(ushort), "ushort" },
        { typeof(void), "void" },
    };
    public static string TypeToCSharp(Type? type)
    {
        if(type == null)
            return "object";
        return primitiveTypes.GetValueOrDefault(type) ?? type.Name;
    }

    public static bool IsSummableType(Type type)
    {
        return IsInteger(type) || IsFloating(type);
    }

    public static bool IsInteger(Type type)
    {
        return
        type == typeof(sbyte) || type == typeof(byte) ||
        type == typeof(short) || type == typeof(ushort) ||
        type == typeof(int) || type == typeof(uint) ||
        type == typeof(long) || type == typeof(ulong) ||
        type == typeof(char);
    }

    public static bool IsFloating(Type type)
    {
        return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
    }

    public static Type? InferTypeFrom(IEnumerable<Type> types)
    {
        Type? def = null; //typeof(object);
        //types = types.Where(t => t != def);

        if(types.Count() == 0)
            return def;
        if(types.All(t => IsSummableType(t)))
        {
            if(types.Any(t => IsFloating(t)))
                return typeof(float);
            else
                return typeof(int);
        }
        return types.Where(t => t != typeof(object)).FirstOrDefault(def);
    }

    public static Type GetParamType(ParameterInfo pInfo)
    {
        if(pInfo.GetCustomAttribute<BBBuffNameAttribute>() != null)
            return typeof(BBBuffName);
        if(pInfo.GetCustomAttribute<BBSpellNameAttribute>() != null)
            return typeof(BBSpellName);
        else
            return pInfo.ParameterType;
    }

    public static string ObjectToCSharp(object? value)
    {
        if(value == null)
            return "null";
        else if(value is BBSpellName sn)
            return $"nameof(Spells.{PrepareName(sn.Value, true)})";
        else if(value is BBBuffName bn)
            return $"nameof(Buffs.{PrepareName(bn.Value, true)})";
        else if(value is string s)
        {
            if(s.StartsWith("$") && s.EndsWith("$"))
                return s.Substring(1, s.Length - 1 - 1);
            else
                return ("\"" + s + "\"");
        }
        else if(value is bool b)
            return b ? "true" : "false";
        else
        {
            var type = value.GetType();
            if(type.IsEnum)
                return string.Join(" | ", value.ToString()!.Split(", ").Select(x => type.Name + "." + x));
            else if(IsFloating(type))
                return value.ToString() + "f";
            else
                return value.ToString()!;
        }
    }

    public static string Braces(string body)
    {
        body = body.Trim();
        if(body.Length > 0)
            return "{\n" + body.Indent() + "\n}";
        return "{\n}";
    }

    public static string Class(string name, string extends = "", string body = "", bool isPublic = true, bool isPartial = false)
    {
        var output = "";
        if(isPublic) output += "public ";
        if(isPartial) output += "partial ";
        output += $"class " + PrepareName(name, true);
        if(extends != "") output += ": " + extends;
        output += "\n" + Braces(body);
        return output;
    }

    public static object? UseValueOrDefault(this Dictionary<string, object> dict, HashSet<string> used, string key)
    {
        used.Add(key);
        return dict.GetValueOrDefault(key);
    }
}