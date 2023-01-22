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

    HashSet<Union<Var, Composite>> Assigned = new();
    public void Assign(Union<Var, Composite> var)
    {
        Initialized = true;
        Assigned.Add(var);
    }
    public void InferType()
    {
        if(Type != null)
            return;
        foreach(var v in Assigned)
        {
            if(v.Item1 == this) //TODO: Better way to resolve ring
                continue;

            Type? type; //TODO: Reduce code
            if(v.Item1 != null)
            {
                v.Item1.InferType();
                type = v.Item1.Type;
            }
            else // if(v.Item2 != null)
            {
                v.Item2!.InferType();
                type = v.Item2.Type;
            }
            if(type != null)
                Types.Add(type);
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
            var funcName = Parent!.ParentScript.Functions.First(kv => kv.Value == Parent).Key;
            return funcName + "_" + name + " " + name + " = new();"; // + " //USEDBY: " + string.Join(" ", Buffs);;
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

    HashSet<string> Buffs = new();
    public void UseAsInstanceVarsFor(string v)
    {
        Buffs.Add(v);
    }
}