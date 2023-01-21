using static Utils;

public class EffectReference: Reference
{
    public int ID;
    public Type? Type;
    public object[] Values;
    public EffectReference(int id, object[] values)
    {
        ID = id;
        Values = values;

        Type = InferTypeFrom(values.Select(v => v.GetType()));
        
        TableName = null;
        VarName = "Level";
    }
    public override string ToCSharp()
    {
        return $"this.{PrepareName("Effect", false)}{ID}[{PrepareName("Level", false)}]";
    }

    public string ToCSharpDecl()
    {
        return
        "public " + TypeToCSharp(Type) + "[] " + PrepareName("Effect", false) + ID + " = {" +
        string.Join(", ", Values.Select(v => ObjectToCSharp(v))) +
        "};";
    }
}