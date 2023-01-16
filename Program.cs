using System.Text;
using System.Reflection;
using Newtonsoft.Json;

var codeJson = File.ReadAllText("code.json", Encoding.UTF8);
var obj = JsonConvert.DeserializeObject<Dictionary<string, Block[]>>(codeJson);

string output = "";

foreach(var kv in obj!)
{
    output += $"public override void {kv.Key}()\n";
    output += "{\n";
    output += ConvertBlocks(kv.Value);
    output += "}\n";
}

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
    var parameters = new List<string>();
    return name + "(" + ")";
}

class Block
{
    public string Function;
    public Dictionary<string, object> Params;
    public Block[]? SubBlocks;
}