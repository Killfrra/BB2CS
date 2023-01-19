using System.Text;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

public class BBScripts
{
    public Dictionary<string, BBScriptComposite> Scripts = new();
    public void Scan()
    {
        foreach(var script in Scripts.Values)
            script.Scan(this);
    }
    public string ToCSharp()
    {
        return
        "using System.Numerics;\n" +
        "using static Functions;\n" +
        "\n" +
        string.Join("\n", Scripts.Select(
            kv => kv.Value.ToCSharp(kv.Key)
        )) + "\n";
    }
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

    public BBScripts Parent;

    public void Scan(BBScripts parent)
    {
        Parent = parent;
        CharScript.Scan(this);
        ItemScript.Scan(this);
        BuffScript.Scan(this);
        SpellScript.Scan(this);
    }

    public string ToCSharp(string name)
    {
        if(Regex.IsMatch(name, @"^\d"))
            name = "_" + name;
        
        return
        CharScript.ToCSharp("Chars", name) + "\n" +
        ItemScript.ToCSharp("Items", name) + "\n" +
        BuffScript.ToCSharp("Buffs", name) + "\n" +
        SpellScript.ToCSharp("Spells", name);
    }
}

public class BBScript
{
    public Dictionary<string, object> Metadata = new();
    public Dictionary<string, BBFunction> Functions = new();
    public Dictionary<string, Union<Var, VarTable>> InstanceVars = new();
    public List<Var> InstanceEffects = new();

    public BBScriptComposite Parent;

    public void Scan(BBScriptComposite parent)
    {
        Parent = parent;
        foreach(var function in Functions.Values)
            function.Scan(this, null);
    }

    public string ToCSharp(string ns, string name)
    {
        return
        "namespace " + ns + "\n" +
        "{" + "\n" +
            ("public class " + name + "\n" +
            "{" + "\n" +
                string.Join("\n", Functions.Select(
                    kv => kv.Value.ToCSharp(kv.Key)
                )).Indent() +
            "\n" + "}").Indent() +
        "\n" + "}";
    }
}
/*
public class BBSpellScript
{
    public Dictionary<string, Union<Var, VarTable>> SpellVars = new();
}
*/
public class BBFunction: SubBlocks
{
    //public string Name;

    public Dictionary<string, Union<Var, VarTable>> LocalVars = new();

    public string ToCSharp(string name)
    {
        return
        "public void " + name + "()\n" +
        BaseToCSharp();
    }
}

public class Block
{
    public string Function;
    public Dictionary<string, object> Params = new();
    public List<Block> SubBlocks = new();

    public string ResolvedName;
    public List<Union<Composite, SubBlocks>> ResolvedParams = new();
    public Reference? ResolvedReturn;

    public SubBlocks Parent;

    public void Scan(SubBlocks parent)
    {
        Parent = parent;
        ResolvedName = Function.Substring(3, Function.Length - 3 - 1);

        var mInfo = typeof(Functions).GetMethod(
            ResolvedName, BindingFlags.Public | BindingFlags.Static
        );
        if(mInfo == null)
        {
            throw new Exception($"{ResolvedName} is undefiend");
        }

        foreach(var pInfo in mInfo.GetParameters())
        {
            var sAttr = pInfo.GetCustomAttribute<BBSubBlocks>();
            if(sAttr != null)
            {
                var sb = new SubBlocks();
                    sb.Blocks = SubBlocks;
                    sb.Scan(parent);
                foreach(var paramNameName in sAttr.ParamNames)
                {
                    var paramName = (string)Params[paramNameName + "Var"];
                    var arg = new Var();
                        arg.Write(typeof(object)); //TODO:
                    sb.Args[paramName] = arg;
                }
                ResolvedParams.Add(sb);
            }
            else
            {
                var value = new Composite(pInfo, Params);
                ResolvedParams.Add(value);
            }

            var returnType = mInfo.ReturnType;
            if(returnType != typeof(void))
            {
                var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>() ?? new();
                ResolvedReturn = Reference.Resolve(fAttr.Dest, Params);
            }
        }
    }

    public string ToCSharp()
    {
        return ResolvedName + "(" +
        string.Join(", ", ResolvedParams.Select(p => p.Item1?.ToCSharp() ?? p.Item2?.ToCSharp())) + ");";
    }
}

public class Var
{
    //public string Name;

    public Type? Type = null;
    public bool Initialized = false;
    public bool Used = false;
    public void Read(Type type)
    {
        Used = true;
    }
    public void Write(Type type)
    {
        Initialized = true;
    }

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

public class SubBlocks
{
    public List<Block> Blocks = new();
    public Dictionary<string, Var> Args = new();
    
    public BBScript ParentScript;
    public SubBlocks? ParentFunction;

    public virtual void Scan(SubBlocks parentFunc)
    {
        Scan(parentFunc.ParentScript, parentFunc);
    }
    public void Scan(BBScript parentScript, SubBlocks? parentFunc = null)
    {
        ParentScript = parentScript;
        ParentFunction = parentFunc;
        foreach(var block in Blocks)
            block.Scan(this);
    }

    public virtual string BaseToCSharp()
    {
        return
        "{" + "\n" +
        string.Join("\n", Blocks.Select(
            block => block.ToCSharp()
        )).Indent() +
        "\n" + "}";
    }

    public virtual string ToCSharp()
    {
        return "() => " + BaseToCSharp();
    }
}

public class Composite
{
    public Type Type;
    public object? Value;
    public Reference? Var;
    public EffectReference? VarByLevel;

    public Composite(ParameterInfo pInfo, Dictionary<string, object> ps)
    {
        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? new();

        var name = pInfo.Name!.UCFirst();
        var value = (pAttr.ValuePostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValuePostfix) : null;
        var valueByLevel = (pAttr.ValueByLevelPostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValueByLevelPostfix) : null;
        var varName = (pAttr.VarPostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarTablePostfix) as string : null;

        Type = pInfo.ParameterType;
        Value = value;
        Var = (varName != null && varName != "Nothing") ? new Reference(tableName, varName) : null;
        
        VarByLevel = (valueByLevel != null) ?
            new EffectReference(((JArray)valueByLevel).ToObject<object[]>()!)
            : null;
    }

    public string ToCSharp()
    {
        return ""; //TODO:
    }
}

public class Reference
{
    public Type? Type;
    public string? TableName;
    public string VarName;
    protected Reference(){}
    public Reference(string? tableName, string varName)
    {
        TableName = tableName;
        VarName = varName;
    }

    public static Reference? Resolve(string name, Dictionary<string, object> ps)
    {
        name = name.UCFirst();
        var varName = ps.GetValueOrDefault(name + "Var") as string;
        var tableName = ps.GetValueOrDefault(name + "VarTable") as string;
        if(varName != null && varName != "Nothing")
        {
            return new Reference(tableName, varName);
        }
        else
            return null;
    }
}

public class EffectReference: Reference
{
    public int ID;
    public object[] Values;
    public EffectReference(object[] values)
    {
        Values = values;
        
        TableName = "InstanceVars";
        VarName = $"Effect{ID}";
    }
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

    public static new Dictionary<string, MethodInfo> Methods = new();

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        (new Program_v2()).Run();
    }
    public void Run()
    {
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
                    Methods[mAttr.Name] = method;
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
                        
                        var method = Methods.GetValueOrDefault(func.Key);
                        var declType = method?.DeclaringType;
                        if(method == null || declType == null)
                        {
                            Console.WriteLine($"Unclassified function {func.Key}");
                            continue;
                        }
                        var bbfunc = new BBFunction();
                            bbfunc.Blocks = func.Value;
                        var funcName = method.Name;

                        if(declType == typeof(BBCharScript))
                            script.CharScript.Functions.Add(funcName, bbfunc);
                        else if(declType == typeof(BBItemScript))
                            script.ItemScript.Functions.Add(funcName, bbfunc);
                        else if(declType == typeof(BBSpellScript))
                            script.SpellScript.Functions.Add(funcName, bbfunc);
                        else if(declType == typeof(BBBuffScript))
                            script.BuffScript.Functions.Add(funcName, bbfunc);
                    }
                }
            }

            File.WriteAllText(cacheFile, JsonConvert.SerializeObject(scripts, Formatting.Indented), Encoding.UTF8);
        }

        scripts.Scan();
        File.WriteAllText("Code.cs", scripts.ToCSharp(), Encoding.UTF8);
    }
}