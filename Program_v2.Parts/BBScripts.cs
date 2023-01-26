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
        foreach(var script in Scripts.Values)
            foreach(var v in script.BuffScript.InstanceVars.Vars.Values)
                if(!v.Initialized)
                    v.PassedFromOutside = true;
        //HACK:
        //TODO: Make a few scan steps
        foreach(var sba in Block.AllSpellBuffAdds)
            sba.ScanSpellBuffAdd();
    }

    public string ToCSharp()
    {
        return
        """
        #nullable disable
        
        using System.Numerics;
        using static Functions;
        using static Functions_CS;
        using Math = System.Math;
        
        """ + "\n" +
        $"public partial class {PrepareName("BBScript", true)}\n" +
        Braces(
            $"public AllCharVars {PrepareName("CharVars", false)};\n" +
            $"public AllAvatarVars {PrepareName("AvatarVars", false)};"
        ) + "\n" +
        $"public class {PrepareName("AllCharVars", true)}\n" +
        Braces(
            string.Join("\n", CharVars.Vars.Select(
                kv => kv.Value.ToCSharp(kv.Key, true, true)
            ))
        ) + "\n" +
        $"public class {PrepareName("AllAvatarVars", true)}\n" +
        Braces(
            string.Join("\n", AvatarVars.Vars.Select(
                kv => kv.Value.ToCSharp(kv.Key, true, true)
            ))
        ) + "\n" +
        "namespace Buffs" + "\n" +
        Braces(
            string.Join("\n", EmptyBuffScriptNames.Select(
                name => Class(name, "BBScript", "")
            ))
        ) + "\n" +
        string.Join("\n", Scripts.Select(
            kv => kv.Value.ToCSharp(kv.Key)
        )) + "\n";
    }
}