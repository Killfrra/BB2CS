using System.Text;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string Indent(this string str, int count = 4)
    {
        return Regex.Replace(str, @"^", "".PadRight(count), RegexOptions.Multiline);
    }
}

class Program
{
    static BBParamAttribute defaultPAttr = new();
    static BBFuncAttribute defaultFAttr = new();

    static Dictionary<string, Type> instanceVars = new();
    static Dictionary<string, Type> localVars = new();

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
    static string TypeToString(Type type)
    {
        return primitiveTypes.GetValueOrDefault(type) ?? type.Name;
    }
    static bool IsSummableType(Type type)
    {
        return type == typeof(int) || type == typeof(float); //TODO:
    }

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        var codeJson = File.ReadAllText("code.json", Encoding.UTF8);
        var obj = JsonConvert.DeserializeObject<Dictionary<string, Block[]>>(codeJson);

        string output = "";

        foreach(var kv1 in obj!)
        {
            var funcName = kv1.Key;
            var blocks = ConvertBlocks(kv1.Value);

            if(funcName == "PreLoad")
                continue;

            output +=
            $"public void {funcName}()\n" +
            "{\n" +
                (ConvertVars(localVars) +
                blocks).TrimEnd().Indent() + "\n" +
            "}\n";

            //instanceVars.Clear();
            localVars.Clear();
        }

        output =
        "using System.Numerics;\n" +
        "using static Functions;\n" +
        "\n" +
        "public class Code\n" +
        "{\n" +
            (ConvertVars(instanceVars) +
            output).TrimEnd().Indent() + "\n" +
        "}\n";

        File.WriteAllText("Code.cs", output);
    }

    static string PrepareName(string name)
    {
        name = Regex.Replace(name, @"\W","_");
        if(Regex.IsMatch("" + name[0], @"[^a-zA-Z]"))
        {
            name = "_" + name;
        }
        return name;
    }

    static string ConvertVars(Dictionary<string, Type> vars)
    {
        var output = "";
        foreach(var kv2 in vars)
        {
            var varName = kv2.Key;
            var varType = kv2.Value;

            output += $"{TypeToString(varType)}? {PrepareName(varName)} = null;\n";
        }
        return (output == "") ? "" : ("#region VarDecl\n" + output + "#endregion\n");
    }

    static string ConvertBlocks(Block[] blocks)
    {
        string output = "";
        foreach(var block in blocks)
        {
            output += ConvertBlock(block) + ";\n";
        }
        return output;
    }

    static string ConvertBlock(Block block)
    {
        var name = block.Function.Substring(3, block.Function.Length - 3 - 1);

        var flags = BindingFlags.Public | BindingFlags.Static;
        var mInfo = typeof(Functions).GetMethod(name, flags);
        if(mInfo == null)
        {
            //throw new Exception($"{name} is undefiend");
            //Console.WriteLine(name);
            return "";
        }
        var parameters = new List<object>();
        foreach(var pInfo in mInfo.GetParameters())
        {
            var sAttr = pInfo.GetCustomAttribute<BBSubBlocks>();
            if(sAttr != null)
            {
                var sbArgNames = new List<string>();
                foreach(var paramNameName in sAttr.ParamNames)
                {
                    var paramName = (string)block.Params[paramNameName + "Var"];
                    sbArgNames.Add(paramName);
                }
                parameters.Add(new SubBlocks(block.SubBlocks ?? new Block[0], sbArgNames.ToArray()));
            }
            else
            {
                var value = Resolve(pInfo, block.Params);
                parameters.Add(value);
            }
        }
        string output;
        Type returnType;
        if(name == "SetVarInTable") //HACK:
        {
            var p0 = (parameters[0] as Composite)!;
            output = ObjectToString(p0);
            returnType = p0.DeductType() ?? typeof(object);
        }
        else
        {
            output = name + "(" + string.Join(", ", parameters.Select(p => ObjectToString(p))) + ")";
            returnType = mInfo.ReturnType;
        }
        
        if(returnType != typeof(void))
        {
            var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>() ?? defaultFAttr;
            var ret = ResolveReturn(fAttr.Dest, block.Params);
            if(ret != null)
            {
                output = ret + " = " + output;
                AddType(ret, returnType);
            }
        }
        return output;
    }

    static void AddType(Reference ret, Type type)
    {
        if(ret.TableName == null)
        {
            localVars[ret.VarName] = type;
        }
        if(ret.TableName == "InstanceVars")
        {
            instanceVars[ret.VarName] = type;
        }
    }

    static string UCFirst(string str)
    {
        return Char.ToUpperInvariant(str[0]) + str.Substring(1);
    }

    static Reference? ResolveReturn(string name, Dictionary<string, object> ps)
    {
        name = UCFirst(name);
        var varName = ps.GetValueOrDefault(name + "Var") as string;
        var tableName = ps.GetValueOrDefault(name + "VarTable") as string;
        if(varName != null)
        {
            return new Reference(tableName, varName);
        }
        else
            return null;
    }

    static string ObjectToString(object value)
    {
        if(value is string s)
        {
            if(s.StartsWith("$") && s.EndsWith("$"))
            {
                s = s.Substring(1, s.Length - 1 - 1);
                //s = PrepareName(s);
                var constant = Constants.Table.GetValueOrDefault(s);
                if(constant != null)
                {
                    s = ObjectToString(constant);
                }
                return s;
            }
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
            else
                return value.ToString()!;
        }
    }

    static Composite Resolve(ParameterInfo pInfo, Dictionary<string, object> ps)
    {
        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? defaultPAttr;

        var name = UCFirst(pInfo.Name!);
        var value = (pAttr.ValuePostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValuePostfix) : null;
        var valueByLevel = (pAttr.ValueByLevelPostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValueByLevelPostfix) : null;
        var varName = (pAttr.VarPostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarTablePostfix) as string : null;

        return new Composite(
            type: pInfo.ParameterType,
            value: value,
            var: (varName != null) ? new Reference(tableName, varName) : null,
            varByLevel: (valueByLevel != null) ? new Reference("InstanceVars", "VALUE_BY_LEVEL") : null
        );
    }

    class Block
    {
        public string Function;
        public Dictionary<string, object> Params;
        public Block[]? SubBlocks;
    }

    class SubBlocks
    {
        public Block[] Blocks;
        public string[] ParamNames;
        public SubBlocks(Block[] blocks, string[] paramNames)
        {
            Blocks = blocks;
            ParamNames = paramNames;
        }
        public override string ToString()
        {
            return $"({string.Join(", ", ParamNames.Select(p => PrepareName(p)))}) => {{\n{ConvertBlocks(Blocks).TrimEnd().Indent()}\n}}";
        }
    }

    class Reference
    {
        public string? TableName;
        public string VarName;
        public Reference(string? tableName, string varName)
        {
            TableName = tableName;
            VarName = varName;
        }

        public Type? DeductType()
        {
            if(TableName == null)
            {
                return localVars.GetValueOrDefault(VarName);
            }
            else if(TableName == "InstanceVars")
            {
                return instanceVars.GetValueOrDefault(VarName);
            }
            else
                return null;
        }

        public override string ToString()
        {
            if(TableName != null)
            {
                var tableName = TableName;
                if(tableName == "InstanceVars")
                    tableName = "this";
                return PrepareName(tableName) + "." + PrepareName(VarName);
            }
            else
                return PrepareName(VarName);
        }
    }

    class Composite
    {
        public Type Type;
        public object? Value;
        public Reference? Var;
        public Reference? VarByLevel;

        public Composite(Type type, object? value, Reference? var, Reference? varByLevel)
        {
            Type = type;
            Value = value;
            Var = var;
            VarByLevel = varByLevel;
        }

        public Type? DeductType()
        {
            return Value?.GetType() ?? Var?.DeductType() ?? VarByLevel?.DeductType();
        }

        public override string ToString()
        {
            int count = Convert.ToInt32(Value != null) +
                        Convert.ToInt32(Var != null) +
                        Convert.ToInt32(VarByLevel != null);
            if(count > 0)
            {
                if(count == 1)
                {
                    if(Value != null)
                    {
                        return ObjectToString(Value);
                    }
                    else
                    {
                        var r = (Var ?? VarByLevel)!;
                        return r.ToString();
                    }
                }
                else if(IsSummableType(Type))
                {
                    var output = "0";
                    if(Value != null)
                    {
                        output = ObjectToString(Value);
                    }
                    if(Var != null)
                    {
                        output += $" + ({Var.ToString()} ?? 0)";
                    }
                    if(VarByLevel != null)
                    {
                        output += $" + ({VarByLevel.ToString()} ?? 0)";
                    }
                    return output;
                }
                else
                {
                    var output = new List<string>();
                    if(Var != null)
                    {
                        output.Add(Var.ToString());
                    }

                    if(VarByLevel != null)
                    {
                        output.Add(VarByLevel.ToString());
                    }
                    else if(Value != null)
                    {
                        output.Add(ObjectToString(Value));
                    }
                    return string.Join(" ?? ", output);
                }
            }
            else
                return "default";
        }
    }
}