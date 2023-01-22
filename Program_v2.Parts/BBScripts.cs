using static Utils;

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
        """
        using System.Numerics;
        using static Functions;
        using static Functions_CS;
        """ + "\n" +
        $"public partial class {PrepareName("Script", true)}\n" +
        "{" + "\n" + (
            $"public AllCharVars {PrepareName("CharVars", false)};\n" +
            $"public AllAvatarVars {PrepareName("AvatarVars", false)};"
        ).Indent() + "\n" +
        "}" + "\n" +
        $"public class {PrepareName("AllCharVars", true)}\n" +
        "{" + "\n" +
            string.Join("\n", CharVars.Vars.Select(
                kv => kv.Value.ToCSharp(kv.Key, true)
            )).Indent() + "\n" +
        "}" + "\n" +
        $"public class {PrepareName("AllAvatarVars", true)}\n" +
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