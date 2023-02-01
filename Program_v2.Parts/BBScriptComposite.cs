using Newtonsoft.Json;

public class BBScriptComposite
{
    //public string Name;
    public BBCharScript2 CharScript = new();
    public BBItemScript2 ItemScript = new();
    public BBBuffScript2 BuffScript = new();
    public BBSpellScript2 SpellScript = new();

    public string Path;

    [JsonIgnore]
    public List<BBScript2> Scripts;
    public BBScriptComposite(string path)
    {
        Path = path;
        Scripts = new List<BBScript2>
        {
            CharScript,
            ItemScript,
            BuffScript,
            SpellScript
        };
    }

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
        if(CharScript.Functions.Count > 0 || CharScript.Used)
            output += CharScript.ToCSharp("Chars", name) + "\n";
        if(ItemScript.Functions.Count > 0 || ItemScript.Used)
            output += ItemScript.ToCSharp("Items", name) + "\n";
        if(BuffScript.Functions.Count > 0 || BuffScript.Used)
            output += BuffScript.ToCSharp("Buffs", name) + "\n";
        if(SpellScript.Functions.Count > 0 || SpellScript.Used)
            output += SpellScript.ToCSharp("Spells", name);
        return output.TrimEnd();
    }
}