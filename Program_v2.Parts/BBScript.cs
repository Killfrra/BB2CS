using static Utils;

//TODO: Rename
public class BBSpellScript2: BBScript
{
    public Var SpellVars = new(true);
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