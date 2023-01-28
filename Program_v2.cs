using System.Text;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static Utils;

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

    public static Dictionary<string, Dictionary<Type, MethodInfo>> Methods = new();
    public static BBScripts? scripts = null;

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
                    Methods[mAttr.Name] = Methods.GetValueOrDefault(mAttr.Name) ?? new();
                    Methods[mAttr.Name][scriptType] = method;
                }
            }
        }

        const string cacheFile = "Cache.json";
        if(File.Exists(cacheFile))
        {
            var json = File.ReadAllText(cacheFile, Encoding.UTF8);
            scripts = JsonConvert.DeserializeObject<BBScripts>(json);
        }
        if(scripts == null)
        {
            scripts = new();

            var unused = new Dictionary<string, int>();

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

                    var used = new HashSet<string>();
                    var script = scripts.Scripts[fileName] = new();
                        script.BuffScript.MetaData.Parse(metadata, used);
                        script.SpellScript.MetaData.Parse(metadata, used);
                    foreach(var key in metadata.Keys)
                        if(!used.Contains(key))
                            unused[key] = unused.GetValueOrDefault(key, 0) + 1;

                    foreach(var func in functions)
                    {
                        if(func.Key == "PreLoad")
                            continue;
                        
                        var methods = Methods.GetValueOrDefault(func.Key);
                        if(methods == null)
                        {
                            Console.WriteLine($"Unclassified function {func.Key}");
                            continue;
                        }

                        var method = methods.Values.First();
                        if(methods.Count > 1)
                        {
                            if(!(
                                filePath.Contains("Characters") && methods.TryGetValue(typeof(BBCharScript), out method) ||
                                filePath.Contains("Talents") && methods.TryGetValue(typeof(BBCharScript), out method) || //TODO: BBTalentScript?
                                filePath.Contains("Items") && methods.TryGetValue(typeof(BBItemScript), out method)
                            ))
                                Console.WriteLine($"Can't determine which script type to choose for {func.Key} method");
                        }

                        var declType = method?.DeclaringType;
                        
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

            foreach(var (key, count) in unused)
                Console.WriteLine($"{count.ToString().PadLeft(3, '0')} {key}");

            File.WriteAllText(cacheFile, JsonConvert.SerializeObject(scripts, Formatting.Indented), Encoding.UTF8);
        }

        //return;

        scripts.Scan();

        /*
        var stats = new Dictionary<string, Dictionary<string, int>>();
        foreach(var scriptComposite in scripts.Scripts.Values)
        foreach(var script in scriptComposite.Scripts)
        foreach(var kv1 in script.Functions)
        foreach(var kv2 in kv1.Value.LocalVars)
            if(!kv2.Value.Initialized)
            {
                var func = script.GetType().Name + "." + kv1.Key;
                var v = kv2.Key;
                if(v is "_" or "NextBuffVars")
                    continue;
                stats[func] = stats.GetValueOrDefault(func) ?? new();
                stats[func][v] = stats[func].GetValueOrDefault(v) + 1;
            }
        var output = "";
        foreach(var kv1 in stats)
        foreach(var kv2 in kv1.Value)
            output += ($"{kv2.Value.ToString().PadLeft(4)} {kv1.Key} {kv2.Key}") + "\n";
        Console.Write(output);
        //return;
        //*/

        /*
        foreach(var kv in Block.Unused)
            foreach(var key in kv.Value)
                Console.WriteLine($"{kv.Key}.{key}");
        //return;
        //*/

        bool changed;
        do
        {
            //Console.WriteLine("loop"); // 8 times

            changed = false;
            foreach(var v in Var.All)
            {
                var prevT = v.Type;
                v.InferType();
                changed = changed || (v.Type != prevT);
            }
            foreach(var c in Composite.All)
            {
                var prevT = c.Type;
                c.InferType();
                changed = changed || (c.Type != prevT);
            }
        }
        while(changed);

        

        foreach(var scriptComposite in scripts.Scripts.Values)
            foreach(var script in scriptComposite.Scripts)
                foreach(var (funcName, func) in script.Functions)
                {
                    //HACK: Custom tables inlining
                    foreach(var (vName, v) in func.LocalVars.ToArray())
                        if(!v.IsArgument && v.IsTable && v.IsCustomTable)
                        {
                            func.LocalVars.Remove(vName);
                            foreach(var (svName, sv) in v.Vars)
                                func.LocalVars.Add(vName + "_" + svName, sv);
                        }

                    foreach(var (vName, v) in func.LocalVars.ToArray())
                        if(!v.IsArgument && !(v.UsedInSubBlocks.Count == 1 && v.UsedInSubBlocks.First() == func) && !v.IsTable && v.UsedInSubBlocks.Count > 0)
                        {
                            func.LocalVars.Remove(vName);
                            foreach(var sb in v.UsedInSubBlocks)
                                sb.LocalVars.Add(vName, v);
                        }
                }

        var cs = scripts.ToCSharp();
            //HACK:
            cs = Regex.Replace(cs, @"\blong\b", "int");
            cs = Regex.Replace(cs, @"\bdouble\b", "float");
            //cs = Regex.Replace(cs, @"\b(TeamId)\? (\w+) = null", "$1 $2 = 0");
            //cs = Regex.Replace(cs, @"\b(Vector3)\? (\w+) = null", "$1 $2 = Vector3.Zero");
            //cs = Regex.Replace(cs, @"\b(TeamId|Vector3)\?", "$1");
            //cs = Regex.Replace(cs, @" \?\? TeamId\.TEAM_UNKNOWN", "");
            cs = Regex.Replace(cs, @" \?\? TeamId\.\w+", "");
            cs = Regex.Replace(cs, @" = default;", ";");
            cs = Regex.Replace(cs, @"^ *//object (nextBuffVars|_);\n", "", RegexOptions.Multiline);
            //cs = Regex.Replace(cs, @"//(?!RequireVar)", "");
            cs = Regex.Replace(cs, @"if\((.*) == \1\)", "if(true)");
            cs = Regex.Replace(cs, @"(?:, default)+(?=\))", "");
            cs = Regex.Replace(cs, @"(?<=\()default(?=\))", "");
            //cs = Regex.Replace(cs, @"\b([a-z][A-Z]+)([A-Z]|\b)", m => m.Groups[1].Value.ToLower() + m.Groups[2].Value);
        File.WriteAllText("Code.cs", cs, Encoding.UTF8);
    }
}