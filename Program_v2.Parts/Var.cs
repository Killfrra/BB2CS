using static Utils;

public class VarTable
{
    private VarTable(){}
}
public class Var
{
    //public string Name;

    public Type? Type = null; //TODO: Type != typeof(VarTable) but IsTable == true
    public bool IsTable => Type == typeof(VarTable) || Vars.Count > 0;
    public bool IsArgument = false;
    public Dictionary<string, Var> Vars = new();

    SubBlocks? Parent = null;

    public Var(bool isTable = false, SubBlocks? parent = null)
    {
        if(isTable)
            Type = typeof(VarTable);
        Parent = parent;
    }

    public bool Initialized = false;
    public bool Used = false;

    HashSet<Type> Types = new();
    public void Write(Type type)
    {
        Initialized = true;
        Types.Add(type);
    }

    HashSet<Composite> AssignedVars = new();
    public void Assign(Composite var)
    {
        Initialized = true;
        AssignedVars.Add(var);
    }
    public void InferType()
    {
        if(Type != null)
            return;
        foreach(var v in AssignedVars)
        {
            v.InferType();
            if(v.Type != null)
                Types.Add(v.Type);
        }
        Type = InferTypeFrom(Types);
    }

    public string ToCSharpArg(string name, bool includeType = true)
    {
        var output = "";
        if(includeType)
        {
            InferType();
            output += TypeToCSharp(Type) + " ";
        }
        output += PrepareName(name, false);
        return output;
    }

    public string ToCSharp(string name, bool pub = false)
    {
        if(IsTable)
        {
            //HACK:
            name = PrepareName(name, false);
            var funcName = Parent!.ParentScript.Functions.Where(kv => kv.Value == Parent).First().Key;
            return funcName + "_" + name + " " + name + " = new();";
        }

        var output = "";

        InferType();

        if(!Initialized)
            output += "//";

        if(pub)
            output += "public ";

        if(Type != null)
            output += TypeToCSharp(Type);
        else
            output += "object";

        if(!(Type != null && IsSummableType(Type)))
            output += "?";

        output += " " + PrepareName(name, pub);

        if(Type != null && IsSummableType(Type))
            output += " = 0;";
        else
            output += " = null;";

        //output += " //";
        //foreach(var type in Types)
        //    output += " " + TypeToCSharp(type);

        return output;
    }
}