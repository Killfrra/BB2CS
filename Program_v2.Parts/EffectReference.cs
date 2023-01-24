using static Utils;

public class EffectReference: Reference
{
    public int ID;
    public Type? Type;
    public object[] Values;
    public EffectReference(int id, object[] values, SubBlocks sb): base(null, "Level", sb)
    {
        ID = id;
        Values = values;
        Type = InferTypeFrom(values.Select(v => v.GetType()));
    }
    public override string ToCSharp()
    {
        return $"this.{PrepareName("Effect", false)}{ID}[{PrepareName(VarName, false)}]";
    }

    public string ToCSharpDecl()
    {
        return
        TypeToCSharp(Type) + "[] " + PrepareName("Effect", false) + ID + " = {" +
        string.Join(", ", Values.Select(v => ObjectToCSharp(v))) +
        "};";
    }
}