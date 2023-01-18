using System.Text;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;

public class BBScripts
{
    public Dictionary<string, BBScriptComposite> Scripts = new();
}

public class BBScriptComposite
{
    //public string Name;

    public Dictionary<string, Union<Var, VarTable>> AvatarVars = new();
    public Dictionary<string, Union<Var, VarTable>> CharVars = new();

    public BBScript CharScript = new();
    public BBScript ItemScript = new();
    public BBScript BuffScript = new();
    public BBScript SpellScript = new();
}

public class BBScript
{
    public Dictionary<string, object> Metadata = new();
    public Dictionary<string, BBFunction> Functions = new();
    public Dictionary<string, Union<Var, VarTable>> InstanceVars = new();
    public List<Var> InstanceEffects = new();
}
/*
public class BBSpellScript
{
    public Dictionary<string, Union<Var, VarTable>> SpellVars = new();
}
*/
public class BBFunction
{
    //public string Name;
    public List<Block> Blocks = new();
    public Dictionary<string, Union<Var, VarTable>> LocalVars = new();
}

public class Var
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

public class VarTable
{
    //string Name;

    public bool Initialized = false;
    public bool Used = false;
    public Dictionary<string, Union<Var, VarTable>> Vars = new();
}

public class Block
{
    public string Function;
    public Dictionary<string, object> Params = new();
    public List<Block> SubBlocks = new();
}

public class SubBlocks
{
    public List<Block> Blocks = new();
    public Dictionary<string, Var> Params = new();
}

public class Reference
{
    public Type? Type;
    public string? TableName;
    public string VarName;
}

public class EffectReference: Reference
{
    public int ID;
}

public class Composite
{
    public Type Type;
    public object? Value;
    public Reference? Var;
    public EffectReference? VarByLevel;
}

public class Program_v2
{
    const string _BB_LUA = ".luaobj.lua";
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
        var methodsDict = new Dictionary<string, MethodInfo>();
        var scriptTypes = new Type[]
        {
            typeof(BBCharScript), typeof(BBSpellScript),
            typeof(BBItemScript), typeof(BBBuffScript),
        };
        foreach(var scriptType in scriptTypes){
            var methods = scriptType.GetMethods(BindingFlags.Public|BindingFlags.Instance);
            foreach(var method in methods)
            {
                var mAttr = method.GetCustomAttribute<BBCallAttribute>();
                if(mAttr != null)
                {
                    methodsDict[mAttr.Name] = method;
                }
            }
        }

        const string cacheFile = "Cache.json";
        BBScripts? scripts = null;
        if(File.Exists(cacheFile))
        {
            var json = File.ReadAllText(cacheFile, Encoding.UTF8);
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
                    
                    var fileName = Path.GetFileName(filePath);
                        fileName = fileName.Substring(0, fileName.Length - _BB_LUA.Length);
                    var text = File.ReadAllText(filePath);

                    var (metadata, functions) = BB2JSON.Parse(text);

                    var script = scripts.Scripts[fileName] = new();

                    foreach(var func in functions)
                    {
                        if(func.Key == "PreLoad")
                            continue;
                        
                        var declType = methodsDict.GetValueOrDefault(func.Key)?.DeclaringType;
                        if(declType == null)
                        {
                            Console.WriteLine($"Unclassified function {func.Key}");
                            continue;
                        }
                        var bbfunc = new BBFunction();
                            bbfunc.Blocks = func.Value;
                        
                        if(declType == typeof(BBCharScript))
                            script.CharScript.Functions.Add(func.Key, bbfunc);
                        else if(declType == typeof(BBItemScript))
                            script.ItemScript.Functions.Add(func.Key, bbfunc);
                        else if(declType == typeof(BBSpellScript))
                            script.SpellScript.Functions.Add(func.Key, bbfunc);
                        else if(declType == typeof(BBBuffScript))
                            script.BuffScript.Functions.Add(func.Key, bbfunc);
                    }
                }
            }

            File.WriteAllText(cacheFile, JsonConvert.SerializeObject(scripts, Formatting.Indented), Encoding.UTF8);
        }
    }
}