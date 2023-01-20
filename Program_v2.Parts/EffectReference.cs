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

        if(values.All(v => IsSummableType(v.GetType())))
        {
            if(values.Any(v => IsFloating(v.GetType())))
                Type = typeof(float);
            else
                Type = typeof(int);        
        }
        else
            Type = InferTypeFrom(values.Select(v => v.GetType()));
        
        TableName = null;
        VarName = "Level";
    }
    public override string ToCSharp()
    {
        return $"this.Effect{ID}[Level]";
    }

    public string ToCSharpDecl()
    {
        return
        "public " + TypeToCSharp(Type) + "[] Effect" + ID + " = {" +
        string.Join(", ", Values.Select(v => ObjectToCSharp(v))) +
        "};";
    }
}