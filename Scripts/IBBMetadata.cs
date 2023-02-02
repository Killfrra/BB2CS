using static Utils;

public class IBBMetadata
{
    public bool IsInitialized()
    {
        return this.GetType().GetFields().Any(fInfo => fInfo.GetValue(this) != null);
    }

    public virtual void Parse(Dictionary<string, object> globals, HashSet<string> used)
    {
    }

    public string ToCSharp()
    {
        var output = ToCSharp(this);
        if(output != "")
            output = $"public override {this.GetType().Name} MetaData {{ get; }} = new()\n{Braces(output)};\n";
        return output;
    }

    public string ToCSharp(object obj)
    {
        var output = "";
        var objType = obj.GetType();

        //var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        foreach(var pInfo in objType.GetFields(/*flags*/))
        {
            var value = pInfo.GetValue(obj);
            var type = pInfo.FieldType;
            if(value != null)
            {
                output += $"{pInfo.Name} = ";
                if(type.IsArray)
                {
                    output += "new[]{ ";
                    foreach(var e in (Array)value)
                        output += ObjectToCSharp(e) + ", ";
                    output += "}";
                }
                else if(type == typeof(ChainMissileParameters)) //HACK:
                    output += "new()\n" + Braces(ToCSharp(value));
                else
                    output += ObjectToCSharp(value);
                output += ",\n";
            }
        }
        return output;
    }
}