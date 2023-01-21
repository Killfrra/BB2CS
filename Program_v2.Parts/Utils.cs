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
        type == typeof(int) || type == typeof(long) ||
        type == typeof(float) || type == typeof(double); //TODO:
    }

    public static bool IsFloating(Type type)
    {
        return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
    }

    public static Type? InferTypeFrom(IEnumerable<Type> types)
    {
        if(types.Count() == 0)
            return null;
        if(types.All(t => IsSummableType(t)))
        {
            if(types.Any(t => IsFloating(t)))
                return typeof(float);
            else
                return typeof(int);
        }
        return types.FirstOrDefault();
    }

    public static string ObjectToCSharp(object value)
    {
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
}