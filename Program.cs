using System.Text;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

BBParamAttribute defaultPAttr = new();

var codeJson = File.ReadAllText("code.json", Encoding.UTF8);
var obj = JsonConvert.DeserializeObject<Dictionary<string, Block[]>>(codeJson);

string output = "";

foreach(var kv in obj!)
{
    var blocks = ConvertBlocks(kv.Value);

    output +=
    $"public void {kv.Key}()\n" +
    "{\n" +
        blocks +
    "}\n";
}

output =
"using static Functions;\n\n" +
"public class Code\n" +
"{\n" +
    output +
"}\n";

File.WriteAllText("Code.cs", output);

string ConvertBlocks(Block[] blocks)
{
    string output = "";
    foreach(var block in blocks)
    {
        output += ConvertBlock(block) + ";\n";
    }
    return output;
}

string ConvertBlock(Block block)
{
    var name = block.Function.Substring(3, block.Function.Length - 3 - 1);

    var flags = BindingFlags.Public | BindingFlags.Static;
    var mInfo = typeof(Functions).GetMethod(name, flags);
    if(mInfo == null)
    {
        //throw new Exception($"{name} is undefiend");
        Console.WriteLine(name);
        return "";
    }
    var parameters = new List<string>();
    foreach(var pInfo in mInfo.GetParameters())
    {
        var sAttr = pInfo.GetCustomAttribute<BBSubBlocks>();
        if(sAttr != null)
        {
            parameters.Add($"({string.Join(", ", sAttr.ParamNames)}) => {{\n{ConvertBlocks(block.SubBlocks)}}}");
        }
        else
        {
            var value = Resolve(pInfo, block.Params) ?? "default";
            parameters.Add(value);
        }
    }
    var output = name + "(" + string.Join(", ", parameters) + ")";
    if(mInfo.ReturnType != typeof(void))
    {
        var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>();
        var ret = ResolveReturn(fAttr.Dest, block.Params);
        if(ret != null)
            output = ret + " = " + output;
    }
    return output;
}

static string UCFirst(string str)
{
    return Char.ToUpperInvariant(str[0]) + str.Substring(1);
}

string? ResolveReturn(string name, Dictionary<string, object> ps)
{
    name = UCFirst(name);
    var varName = ps.GetValueOrDefault(name + "Var") as string;
    var tableName = ps.GetValueOrDefault(name + "VarTable") as string;
    if(varName != null)
    {
        if(tableName != null)
        {
            if(tableName == "InstanceVars")
                tableName = "this";
            return tableName + "." + varName;
        }
        else
            return varName;
    }
    else
        return null;
}

string ToString(object value)
{
    if(value is string s)
    {
        if(s.StartsWith("$") && s.EndsWith("$"))
            return s.Substring(1, s.Length - 1 - 1);
        return ("\"" + s + "\"");
    }
    else if(value is bool b)
        return b ? "true" : "false";
    else
        return value.ToString()!;
}

string? Resolve(ParameterInfo pInfo, Dictionary<string, object> ps)
{
    var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? defaultPAttr;

    var name = UCFirst(pInfo.Name);
    var value = (pAttr.ValuePostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValuePostfix) : null;
    var valueByLevel = (pAttr.ValueByLevelPostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValueByLevelPostfix) : null;
    var varName = (pAttr.VarPostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarPostfix) as string : null;
    var tableName = (pAttr.VarTablePostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarTablePostfix) as string : null;

    var output = new List<string?>();
    if(value != null)
    {
        output.Add(ToString(value));
    }
    if(valueByLevel != null)
    {
        output.Add("VALUE_BY_LEVEL");
    }
    if(varName != null)
    {
        if(tableName != null)
        {
            if(tableName == "InstanceVars")
                tableName = "this";
            output.Add(tableName + "." + varName);
        }
        else
            output.Add(varName);
    }
    if(output.Count > 0)
    {
        if(output.Count == 1)
            return output[0];
        else
        {
            var delim = " + ";
            if(!(
                pInfo.ParameterType == typeof(int) ||
                pInfo.ParameterType == typeof(float)
            )){
                delim = " ?? ";
                output.Reverse();
            }
            return string.Join(delim, output);
        }
    }
    else
        return "default";
}

class Block
{
    public string Function;
    public Dictionary<string, object> Params;
    public Block[]? SubBlocks;
}