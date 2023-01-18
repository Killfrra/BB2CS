using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

class BBScripts
{
    Dictionary<string, BBScriptComposite> Scripts = new();
}

class BBScriptComposite
{
    //public string Name;

    public Dictionary<string, Union<Var, VarTable>> AvatarVars = new();
    public Dictionary<string, Union<Var, VarTable>> CharVars = new();

    BBScript? CharScript;
    BBScript? ItemScript;
    BBScript? BuffScript;
    BBScript? SpellScript;
}

class BBScript
{
    public Dictionary<string, object> Metadata = new();
    public Dictionary<string, BBFunction> Functions = new();
    public Dictionary<string, Union<Var, VarTable>> InstanceVars = new();
    public List<Var> InstanceEffects = new();
}
/*
class BBSpellScript
{
    public Dictionary<string, Union<Var, VarTable>> SpellVars = new();
}
*/
class BBFunction
{
    //public string Name;
    public List<Block> Blocks = new();
    public Dictionary<string, Union<Var, VarTable>> LocalVars = new();
}

class Var
{
    //public string Name;

    public Type? Type = null;
    public bool Initialized = false;
    public bool Used = false;
    public void Read(Type type){}
    public void Write(Type type){}

    List<Var> AssignedVars = new();
    public void Assign(Var var){}
    public void Reveal(){}
}

class VarTable
{
    //string Name;

    public bool Initialized = false;
    public bool Used = false;
    Dictionary<string, Union<Var, VarTable>> Vars = new();
}

class Block
{
    public string Function;
    public Dictionary<string, object> Params = new();
    public List<Block> SubBlocks = new();
}

class SubBlocks
{
    public List<Block> Blocks = new();
    public Dictionary<string, Var> Params = new();
}

class Reference
{
    public Type? Type;
    public string? TableName;
    public string VarName;
}

class EffectReference: Reference
{
    public int ID;
}

class Composite
{
    public Type Type;
    public object? Value;
    public Reference? Var;
    public EffectReference? VarByLevel;
}

class Program_v2
{
    const string _BB_LUA = ".lua";
    string[] globs =
    {
        //"Game/DATA/Characters/*/Scripts/*" + _BB_LUA,
        "Game/DATA/Characters/*",
        "Game/DATA/Items/*" + _BB_LUA,
        "Game/DATA/Talents/*" + _BB_LUA,
        "Game/DATA/Spells/*" + _BB_LUA,
    };

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        (new Program_v2()).Run();
    }
    public void Run()
    {
        const string cacheFile = "Cache.json";
        BBScripts? scripts = null;
        if(File.Exists(cacheFile))
        {
            var json = File.ReadAllText(cacheFile);
            scripts = JsonConvert.DeserializeObject<BBScripts>(json);
        }
        if(scripts == null)
        {
            scripts = new();
            var pwd = Directory.GetCurrentDirectory();
            foreach(var glob in globs)
            {
                foreach(var filePath in Directory.EnumerateFiles(pwd, glob, SearchOption.AllDirectories))
                {
                    if(!filePath.EndsWith(_BB_LUA))
                        continue;
                    
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    var text = File.ReadAllText(filePath);

                    var (metadataJson, functionsJson) = BB2JSON.Convert(text);
                    
                    var metadata = JsonConvert.DeserializeObject<Dictionary<string, object>>(metadataJson);
                    var functions = JsonConvert.DeserializeObject<Dictionary<string, Block[]>>(functionsJson); 

                                       
                }
            }
        }
    }
}