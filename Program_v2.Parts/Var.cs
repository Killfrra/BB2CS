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
    public bool IsCustomTable = false;

    public bool IsArgument = false;
    public Dictionary<string, Var> Vars = new();

    SubBlocks? Parent = null;

    public static List<Var> All = new();

    public Var(bool isTable = false, SubBlocks? parent = null)
    {
        All.Add(this);
        
        if(isTable)
            Type = typeof(VarTable);
        Parent = parent;
    }

    public bool Initialized = false;
    public int Used = 0;

    public HashSet<SubBlocks> UsedInSubBlocks = new();
    void UseInSubBlocks(SubBlocks sb)
    {
        SubBlocks? toReplace = null;
        SubBlocks? replaceWith = null;
        foreach(var sb1 in UsedInSubBlocks)
        {
            var ca = GetCommonAncestor(sb, sb1);
            if(ca != null)
            {
                toReplace = sb1;
                replaceWith = ca;
                break;
            }
        }
        if(toReplace == null)
            UsedInSubBlocks.Add(sb);
        else if(toReplace != replaceWith)
        {
            UsedInSubBlocks.Remove(toReplace);
            UsedInSubBlocks.Add(replaceWith!);
        }
        /*
        bool replaced = false;
        UsedInSubBlocks = new(UsedInSubBlocks.Select(
            sb1 => {
                var ca = GetCommonAncestor(sb, sb1);
                replaced = ca != null;
                return ca ?? sb1;
            }
        ));
        if(!replaced)
            UsedInSubBlocks.Add(sb);
        */
        /*
        if(UsedInSubBlocks.FirstOrDefault(parent => IsChild(sb, parent)) == null)
        {
            UsedInSubBlocks.RemoveWhere(child => IsChild(child, sb));
            UsedInSubBlocks.Add(sb);
        }
        */
    }
    SubBlocks? GetCommonAncestor(SubBlocks a, SubBlocks b)
    {
        if(a == b) return a;

        var apath = new List<SubBlocks>();
        for(var c = a; c != null; c = c.ParentBlock)
            apath.Insert(0, c);            
        var bpath = new List<SubBlocks>();
        for(var c = b; c != null; c = c.ParentBlock)
            bpath.Insert(0, c);
        if(apath[0] == bpath[0])
        {
            int i = 1;
            while(i < Math.Min(apath.Count, bpath.Count) && apath[i] == bpath[i])
                i++;
            return apath[i - 1];
        }
        else
            return null;
    }
    bool IsChild(SubBlocks child, SubBlocks parent)
    {
        for(var t = child.ParentBlock; t != null; t = t.ParentBlock)
            if(t == parent)
                return true;
        return false;
    }

    HashSet<Type> Types = new();
    public void Write(Type type, SubBlocks sb)
    {
        UseInSubBlocks(sb);
        Initialized = true;
        Types.Add(type);
    }

    HashSet<Union<Var, Composite>> Assigned = new();
    public void Assign(Union<Var, Composite> var, SubBlocks sb)
    {
        UseInSubBlocks(sb);
        Initialized = true;
        Assigned.Add(var);
    }

    HashSet<Type> ReadedTypes = new();
    public void Read(Type type, SubBlocks sb)
    {
        UseInSubBlocks(sb);
        ReadedTypes.Add(type);
    }

    HashSet<Var> AssignedTo = new();
    public void AssignTo(Var var, SubBlocks sb)
    {
        UseInSubBlocks(sb);
        AssignedTo.Add(var);
    }

    bool inferred = false;
    public void InferType()
    {
        if(inferred || Type != null)
        {
            //inferred = true;
            return;
        }
        foreach(var v in Assigned)
        {
            if(v.Item1 == this) //TODO: Better way to resolve ring
                continue;

            Type? type; //TODO: Reduce code
            if(v.Item1 != null)
            {
                //v.Item1.InferType();
                type = v.Item1.Type;
            }
            else // if(v.Item2 != null)
            {
                //v.Item2!.InferType();
                type = v.Item2!.Type;
            }
            if(type != null)
                Types.Add(type);
        }
        Type = InferTypeFrom(Types);
        
        if(Type == null)
        {
            Types = ReadedTypes;
            foreach(var v in AssignedTo)
                if(v.Type != null)
                    Types.Add(v.Type);
            Type = InferTypeFrom(Types);
        }
        //inferred = true;
    }

    //TODO: Rename and deduplicate
    public string BaseToCSharp(string name, bool ucfirst, bool includeDefault)
    {
        var output = "";

        if(Type != null)
            output += TypeToCSharp(Type);
        else
            output += "object";

        //if(!(Type != null && IsSummableType(Type)))
        //    output += "?";

        output += " " + PrepareName(name, ucfirst);

        if(includeDefault)
        {
            /*
            if(Type != null && IsSummableType(Type))
                output += " = 0";
            else
                output += " = null";
            */
            output += " = default";
        }

        return output;
    }

    public string ToCSharpArg(string name, bool includeType)
    {
        var output = "";
        if(includeType)
        {
            //InferType();
            output += TypeToCSharp(Type) + " ";
        }
        output += PrepareName(name, false);
        return output;
    }

    public string ToCSharp(string name, bool isPublic, bool includeDefault)
    {
        if(IsTable)
        {
            //HACK:
            name = PrepareName(name, false);
            var funcName = Parent!.ParentScript.Functions.First(kv => kv.Value == Parent).Key;
            return funcName + "_" + name + " " + name + " = new();"; // + " //USEDBY: " + string.Join(" ", Buffs);;
        }

        var output = "";

        //InferType();

        //if(!(Initialized || (PassedFromOutside && Used > 0)))
        //    output += "//";

        if(isPublic)
            output += "public ";

        output += BaseToCSharp(name, isPublic, includeDefault) + ";";

        bool initialized = Initialized || PassedFromOutside;
        if(!initialized || Used == 0)
        {
            output += " //";
            if(!initialized) output += " UNITIALIZED";
            if(Used == 0) output += " UNUSED";
        }

        //output += " //USEDIN: " + UsedInSubBlocks.Count;

        //output += " //";
        //foreach(var type in Types)
        //    output += " " + TypeToCSharp(type);

        return output;
    }

    HashSet<string> Buffs = new();
    public bool PassedFromOutside = false;
    public void UseAsInstanceVarsFor(string v)
    {
        Buffs.Add(v);
    }
}