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
        //if(BuffScript.Functions.Count > 0)
        //    output += BuffScript.ToCSharp("Buffs", name) + "\n";
        if(SpellScript.Functions.Count > 0)
            output += SpellScript.ToCSharp("Spells", name);
        return output;
    }
}