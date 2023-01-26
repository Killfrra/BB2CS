using System.Text.RegularExpressions;

public static class Utils
{
    public static string PrepareName(string name, bool ucfirst)
    {
        name = Regex.Replace(name, @"\W","_");
        if(Regex.IsMatch("" + name[0], @"[^a-z_]", RegexOptions.IgnoreCase))
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
        return
        type == typeof(sbyte) || type == typeof(byte) ||
        type == typeof(short) || type == typeof(ushort) ||
        type == typeof(int) || type == typeof(uint) ||
        type == typeof(long) || type == typeof(ulong) ||
        type == typeof(char) || IsFloating(type);
    }

    public static bool IsFloating(Type type)
    {
        return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
    }

    public static bool IsInteger(Type type)
    {
        return IsSummableType(type) && !IsFloating(type);
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

    public static string ObjectToCSharp(object? value)
    {
        if(value == null)
            return "null";
        if(value is string s)
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