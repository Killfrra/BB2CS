using System.Text;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using static Program_v2;
using System.Diagnostics;

public class BBScripts
{
    public Var CharVars = new(true);
    public Var AvatarVars = new(true);
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
        """
        public class Script
        {
            public AttackableUnit Owner;
            public AllCharVars CharVars;
            public AllAvatarVars AvatarVars;
        }
        """ + "\n" +
        "public class AllCharVars" + "\n" +
        "{" + "\n" +
            string.Join("\n", CharVars.Vars.Select(
                kv => kv.Value.ToCSharp(kv.Key, true)
            )).Indent() + "\n" +
        "}" + "\n" +
        "public class AllAvatarVars" + "\n" +
        "{" + "\n" +
            string.Join("\n", AvatarVars.Vars.Select(
                kv => kv.Value.ToCSharp(kv.Key, true)
            )).Indent() + "\n" +
        "}" + "\n" +
        string.Join("\n", Scripts.Select(
            kv => kv.Value.ToCSharp(kv.Key)
        )) + "\n";
    }
}

public class BBScriptComposite
{
    //public string Name;
    public BBScript CharScript = new();
    public BBScript ItemScript = new();
    public BBScript BuffScript = new();
    public BBSpellScript2 SpellScript = new();

    public BBScripts Parent;

    public void Scan(BBScripts parent)
    {
        Parent = parent;
        if(CharScript.Functions.Count > 0)
            CharScript.Scan(this);
        if(ItemScript.Functions.Count > 0)
            ItemScript.Scan(this);
        if(BuffScript.Functions.Count > 0)
            BuffScript.Scan(this);
        if(SpellScript.Functions.Count > 0)
            SpellScript.Scan(this);
    }

    public string ToCSharp(string name)
    {        
        var output = "";
        if(CharScript.Functions.Count > 0)
            output += CharScript.ToCSharp("Chars", name) + "\n";
        if(ItemScript.Functions.Count > 0)
            output += ItemScript.ToCSharp("Items", name) + "\n";
        if(BuffScript.Functions.Count > 0)
            output += BuffScript.ToCSharp("Buffs", name) + "\n";
        if(SpellScript.Functions.Count > 0)
            output += SpellScript.ToCSharp("Spells", name);
        return output;
    }
}

public class BBScript
{
    public Dictionary<string, object> Metadata = new();
    public Dictionary<string, BBFunction> Functions = new();
    public Var InstanceVars = new(true);
    public List<EffectReference> InstanceEffects = new();

    public BBScriptComposite Parent;

    public void Scan(BBScriptComposite parent)
    {
        Parent = parent;
        foreach(var function in Functions.Values)
            function.Scan(this, null);
    }

    public string ToCSharp(string ns, string name)
    {
        var output = "";
        foreach(var func in Functions) //TODO: MultiSelect?
        {
            foreach(var v in func.Value.LocalVars)
            {
                if(v.Value.IsTable)
                {
                    output +=
                    "class " + func.Key + "_" + PrepareName(v.Key) + "\n" +
                    "{" + "\n" +
                        string.Join("\n", v.Value.Vars.Select(
                            kv => kv.Value.ToCSharp(kv.Key, true)
                        )).Indent() + "\n" +
                    "}" + "\n";
                }
            }
        }

        return
        "namespace " + ns + "\n" +
        "{" + "\n" + (
            "public class " + PrepareName(name) + " : Script" + "\n" +
            "{" + "\n" + (
                output +
                string.Join("", InstanceEffects.Select(
                    e => e.ToCSharpDecl() + "\n"
                )) +
                string.Join("", InstanceVars.Vars.Select(
                    kv => kv.Value.ToCSharp(kv.Key) + "\n"
                )) +
                string.Join("\n", Functions.Select(
                    kv => kv.Value.ToCSharp(kv.Key)
                ))).Indent() + "\n" +
            "}").Indent() + "\n" +
        "}";
    }
}

public class BBSpellScript2: BBScript //TODO: Rename
{
    public Var SpellVars = new(true);
}

public class BBFunction: SubBlocks
{
    //public string Name;
    public override bool Locked => false;
}

public class Block
{
    public string Function;
    public Dictionary<string, object> Params = new();
    public List<Block> SubBlocks = new();

    public string ResolvedName;
    public List<Union<Composite, SubBlocks, Reference>> ResolvedParams = new();
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
                    var arg = new Var(parent: sb);
                        arg.IsArgument = true;
                        arg.Initialized = true; //arg.Write(typeof(object)); //TODO:
                    sb.LocalVars[paramName] = arg;
                }
                ResolvedParams.Add(sb);
            }
            else
            {
                if(pInfo.IsOut || pInfo.ParameterType.IsByRef)
                {
                    var value = Reference.Resolve(pInfo, Params, Parent) ?? new Reference(null, "_");
                        value.IsOut = pInfo.IsOut && pInfo.ParameterType.IsByRef;
                        value.IsRef = !pInfo.IsOut && pInfo.ParameterType.IsByRef;
                    ResolvedParams.Add(value);
                }
                else
                {
                    var value = new Composite(pInfo, Params, Parent);
                    ResolvedParams.Add(value);
                }
            }

            var returnType = mInfo.ReturnType;
            if(returnType != typeof(void))
            {
                var param0 = (ResolvedParams.Count > 0) ? ResolvedParams[0].Item1 : null;
                ResolvedReturn = Reference.Resolve(mInfo, Params, Parent, param0);
            }
        }
    }

    public string ToCSharp()
    {
        return
        ((ResolvedReturn != null) ? (ResolvedReturn.ToCSharp() + " = ") : "") +
        ResolvedName + "(" +
            string.Join(", ", ResolvedParams.Select(p => p.Item1?.ToCSharp() ?? p.Item2?.ToCSharp() ?? p.Item3!.ToCSharp())) +
        ");";
    }
}

public class Var
{
    //public string Name;

    public Type? Type = null;
    public bool IsTable => Type == typeof(VarTable);
    public bool IsArgument = false;
    public Dictionary<string, Var> Vars = new();

    SubBlocks? Parent = null;

    public Var(bool isTable = false, SubBlocks? parent = null)
    {
        if(isTable)
            Type = typeof(VarTable);
        Parent = parent;
    }

    public bool Initialized = false;
    public bool Used = false;

    HashSet<Type> Types = new();
    public void Write(Type type)
    {
        Initialized = true;
        Types.Add(type);
    }

    HashSet<Composite> AssignedVars = new();
    public void Assign(Composite var)
    {
        Initialized = true;
        AssignedVars.Add(var);
    }
    public void InferType()
    {
        if(Type != null)
            return;
        foreach(var v in AssignedVars)
        {
            v.InferType();
            if(v.Type != null)
                Types.Add(v.Type);
        }
        Type = InferTypeFrom(Types);
    }

    public string ToCSharpArg(string name, bool includeType = true)
    {
        var output = "";
        if(includeType)
        {
            InferType();
            output += TypeToCSharp(Type) + " ";
        }
        output += PrepareName(name);
        return output;
    }

    public string ToCSharp(string name, bool pub = false)
    {
        if(IsTable)
        {
            //HACK:
            name = PrepareName(name);
            var funcName = Parent!.ParentScript.Functions.Where(kv => kv.Value == Parent).First().Key;
            return funcName + "_" + name + " " + name + " = new();";
        }

        var output = "";

        InferType();

        if(!Initialized)
            output += "//";

        if(pub)
            output += "public ";

        if(Type != null)
            output += TypeToCSharp(Type);
        else
            output += "object";

        if(!(Type != null && IsSummableType(Type)))
            output += "?";

        output += " " + PrepareName(name);

        if(Type != null && IsSummableType(Type))
            output += " = 0;";
        else
            output += " = null;";

        //output += " //";
        //foreach(var type in Types)
        //    output += " " + TypeToCSharp(type);

        return output;
    }
}

public class VarTable
{
    private VarTable(){}
}

public class SubBlocks
{
    public List<Block> Blocks = new();
    public Dictionary<string, Var> LocalVars = new();
    public virtual bool Locked => true;
    
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

    public string ArgsToCSharp(bool includeType = true)
    {
        return
        "(" + string.Join(", ", LocalVars.Where(
            kv => kv.Value.IsArgument
        ).Select(
            kv => kv.Value.ToCSharpArg(kv.Key, includeType)
        )) + ")";
    }

    public string BaseToCSharp()
    {
        return
        "{" + "\n" + (
            string.Join("", LocalVars.Where(
                kv => !kv.Value.IsArgument
            ).Select(
                kv => kv.Value.ToCSharp(kv.Key) + "\n"
            )) +
            string.Join("\n", Blocks.Select(
                block => block.ToCSharp()
            ))).Indent() + "\n" +
        "}";
    }

    public string ToCSharp(string name)
    {
        return
        "public void " + name + ArgsToCSharp(true) + "\n" +
        BaseToCSharp();
    }

    public virtual string ToCSharp()
    {
        return
        ArgsToCSharp(false) + " => " + "\n" +
        BaseToCSharp();
    }

    private Var GetOrCreate(Dictionary<string, Var> table, string name)
    {
        return table.GetValueOrDefault(name) ?? (table[name] = new Var(parent: this));
    }
    private Var? Resolve(string name)
    {
        return LocalVars.GetValueOrDefault(name) ?? ParentFunction?.Resolve(name);
    }
    private Var? Declare(string name, bool isTable)
    {
        if(!Locked)
            return (LocalVars[name] = new Var(isTable, parent: this));
        else if(ParentFunction != null)
            return ParentFunction.Declare(name, isTable);
        else
            return null;
    }
    private Var ResolveOrDeclare(string name, bool isTable)
    {
        var v = Resolve(name) ?? Declare(name, isTable)!;
        //Debug.Assert(v.IsTable == isTable);
        return v;
    }
    public virtual Var Resolve(Reference r)
    {
        if(r.TableName != null)
        {
            Var? table = null;
            if(r.TableName == "InstanceVars")
                table = ParentScript.InstanceVars;
            else if(r.TableName == "CharVars")
                table = ParentScript.Parent.Parent.CharVars;
            else if(r.TableName == "AvatarVars")
                table = ParentScript.Parent.Parent.AvatarVars;
            else if(r.TableName == "SpellVars" && ParentScript is BBSpellScript2 ss)
                table = ss.SpellVars;
            else
            {
                table = ResolveOrDeclare(r.TableName, true);
                //table.Initialized = true; //TODO:
                //table.Used = true;
            }
            return GetOrCreate(table.Vars, r.VarName);
        }
        return ResolveOrDeclare(r.VarName, false);
    }
}

public class Composite
{
    public Type? Type;
    public object? Value;
    public Reference? Var;
    public EffectReference? VarByLevel;

    public Composite(ParameterInfo pInfo, Dictionary<string, object> ps, SubBlocks sb)
    {
        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? new();

        var name = pInfo.Name!.UCFirst();
        var value = (pAttr.ValuePostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValuePostfix) : null;
        var valueByLevel = (pAttr.ValueByLevelPostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValueByLevelPostfix) : null;
        var varName = (pAttr.VarPostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarTablePostfix) as string : null;

        Type = pInfo.ParameterType;
        if(Type.Name == "T") //HACK:
            Type = null;

        Value = AskConstants(value);

        if(varName != null && varName != "Nothing")
        {
            Var = new Reference(tableName, varName);
            var v = sb.Resolve(Var);
            v.Used = true;
        }
        if(valueByLevel != null)
        {
            var arr = ((JArray)valueByLevel).ToObject<object[]>()!;
            var id = sb.ParentScript.InstanceEffects.Count;
            VarByLevel = new EffectReference(id, arr);
            sb.ParentScript.InstanceEffects.Add(VarByLevel);
        }
    }

    public string ToCSharp()
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
                    return ObjectToCSharp(Value);
                }
                else
                {
                    var r = (Var ?? VarByLevel)!;
                    return r.ToCSharp();
                }
            }
            else if(IsSummableType(Type))
            {
                var output = new List<string>();
                if(Value != null && Convert.ToSingle(Value) != 0)
                {
                    output.Add(ObjectToCSharp(Value));
                }
                if(Var != null)
                {
                    output.Add(Var.ToCSharp());
                }
                if(VarByLevel != null)
                {
                    output.Add(VarByLevel.ToCSharp());
                }
                return string.Join(" + ", output);
            }
            else
            {
                var output = new List<string>();
                if(Var != null)
                {
                    output.Add(Var.ToCSharp());
                }
                if(VarByLevel != null)
                {
                    output.Add(VarByLevel.ToCSharp());
                }
                else if(Value != null)
                {
                    output.Add(ObjectToCSharp(Value));
                }
                return string.Join(" ?? ", output);
            }
        }
        else
            return "default";
    }

    object? AskConstants(object? value)
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

    public void InferType()
    {
        if(Type != null)
            return;
        List<Type> types = new();
        if(Value != null)
            types.Add(Value.GetType());
        if(VarByLevel != null && VarByLevel.Type != null)
            types.Add(VarByLevel.Type);
        
        //TODO: Var.Var.InferType
        
        Type = InferTypeFrom(types);
    }
}

public class Reference
{
    public string? TableName;
    public string VarName;
    public bool IsOut = false;
    public bool IsRef = false;
    protected Reference(){}
    public Reference(string? tableName, string varName)
    {
        TableName = tableName;
        VarName = varName;
    }

    //TODO: Deduplicate
    public static Reference? Resolve(ParameterInfo pInfo, Dictionary<string, object> ps, SubBlocks sb)
    {
        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? new();

        var name = pInfo.Name!.UCFirst();
        var varName = (pAttr.VarPostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarTablePostfix) as string : null;
    
        if(varName != null && varName != "Nothing")
        {
            var r = new Reference(tableName, varName);
            var type = pInfo.ParameterType;
            if(pInfo.IsOut || type.IsByRef)
            {
                type = type.GetElementType();
                if(type != null && type.Name != "T") //HACK:
                {
                    var v = sb.Resolve(r);
                        v.Write(type);
                }
            }
            return r;
        }
        else
            return null;
    }

    public static Reference? Resolve(MethodInfo mInfo, Dictionary<string, object> ps, SubBlocks sb, Composite? param0)
    {
        var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>() ?? new();

        var name = fAttr.Dest.UCFirst();
        var varName = ps.GetValueOrDefault(name + "Var") as string;
        var tableName = ps.GetValueOrDefault(name + "VarTable") as string;
        if(varName != null && varName != "Nothing")
        {
            var r = new Reference(tableName, varName);
            var v = sb.Resolve(r);
            if(mInfo.Name == nameof(Functions.SetVarInTable))
                v.Assign(param0!);
            else
                v.Write(mInfo.ReturnType);
            return r;
        }
        else
            return null;
    }

    public virtual string ToCSharp()
    {
        var output = "";

        if(IsOut) output += "out ";
        if(IsRef) output += "ref ";

        if(TableName != null)
        {
            var tableName = TableName;
            if(tableName == "InstanceVars")
                tableName = "this";
            output += tableName + "." + PrepareName(VarName);
        }
        else
            output += PrepareName(VarName);

        return output;
    }
}

public class EffectReference: Reference
{
    public int ID;
    public Type? Type;
    public object[] Values;
    public EffectReference(int id, object[] values)
    {
        ID = id;
        Values = values;

        Type = InferTypeFrom(
            values.Select(v => v.GetType())
        );
        TableName = null;
        VarName = "Level";
    }
    public override string ToCSharp()
    {
        return $"this.Effect{ID}[Level]";
    }

    public string ToCSharpDecl()
    {
        return
        "public " + TypeToCSharp(Type) + "[] Effect" + ID + " = {" +
        string.Join(", ", Values.Select(v => ObjectToCSharp(v))) +
        "};";
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

    public static string PrepareName(string name)
    {
        name = Regex.Replace(name, @"\W","_");
        if(Regex.IsMatch("" + name[0], @"[^a-z_]", RegexOptions.IgnoreCase))
        {
            name = "_" + name;
        }
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