using System.Text;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
        var cs = scripts.ToCSharp();
            //HACK:
            cs = Regex.Replace(cs, @"\blong\b", "int");
            cs = Regex.Replace(cs, @"\bdouble\b", "float");
            cs = Regex.Replace(cs, @"\b(TeamId|Vector3)\? (\w+) = null;", "$1 $2;");
            cs = Regex.Replace(cs, @" \?\? TeamId\.\w+", "");
            cs = Regex.Replace(cs, @"\bdamage\.SourceType\b", "damageSource");
        File.WriteAllText("Code.cs", cs, Encoding.UTF8);
    }
}