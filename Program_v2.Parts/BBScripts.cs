using static Utils;

public class BBScripts
{
    public Var CharVars = new(true);
    public Var AvatarVars = new(true);
    public Dictionary<string, BBScriptComposite> Scripts = new();
    public HashSet<string> EmptyBuffScriptNames = new();

    public void Scan()
    {
        foreach(var script in Scripts.Values)
            script.Scan(this);
    }

    private string Brackets(string body)
    {
        body = body.Trim();
        if(body.Length > 0)
            return "{\n" + body.Indent() + "\n}";
        return "{\n}";
    }

    private string Class(string name, string body)
    {
        return
        $"public class {PrepareName(name, true)}\n" +
        Brackets(body);
    }

    public string ToCSharp()
    {
        return
        """
        using System.Numerics;
        using static Functions;
        using static Functions_CS;
        """ + "\n" +
        $"public partial class {PrepareName("Script", true)}\n" +
        Brackets(
            $"public AllCharVars {PrepareName("CharVars", false)};\n" +
            $"public AllAvatarVars {PrepareName("AvatarVars", false)};"
        ) + "\n" +
        $"public class {PrepareName("AllCharVars", true)}\n" +
        Brackets(
            string.Join("\n", CharVars.Vars.Select(
                kv => kv.Value.ToCSharp(kv.Key, true, true)
            ))
        ) + "\n" +
        $"public class {PrepareName("AllAvatarVars", true)}\n" +
        Brackets(
            string.Join("\n", AvatarVars.Vars.Select(
                kv => kv.Value.ToCSharp(kv.Key, true, true)
            ))
        ) + "\n" +
        "namespace Buffs" + "\n" +
        Brackets(
            string.Join("\n", EmptyBuffScriptNames.Select(
                name => Class(name, "")
            ))
        ) + "\n" +
        string.Join("\n", Scripts.Select(
            kv => kv.Value.ToCSharp(kv.Key)
        )) + "\n";
    }
}