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