

//TODO: Deprecate
public class BBFunction: SubBlocks
{
    //public string Name;
    public override bool Locked => false;
}

public class SubBlocks
{
    public List<Block> Blocks = new();
    public Dictionary<string, Var> LocalVars = new();
    public virtual bool Locked => true;
    
    public BBScript2 ParentScript;
    public SubBlocks? ParentFunction;

    public virtual void Scan(SubBlocks parentFunc)
    {
        Scan(parentFunc.ParentScript, parentFunc);
    }
    public void Scan(BBScript2 parentScript, SubBlocks? parentFunc = null)
    {
        ParentScript = parentScript;
        ParentFunction = parentFunc;
        foreach(var block in Blocks)
            block.Scan(this);
    }

    public string ArgsToCSharp(bool includeType = true)
    {
        return
        "(" + string.Join(", ", LocalVars.Where(
            kv => kv.Value.IsArgument
        ).Select(
            kv => kv.Value.ToCSharpArg(kv.Key, includeType)
        )) + ")";
    }

    public string BaseToCSharp()
    {
        return
        "{" + "\n" +
            string.Join("\n", LocalVars.Where(
                kv => !kv.Value.IsArgument
            ).Select(
                kv => kv.Value.ToCSharp(kv.Key)
            ).Concat(Blocks.Select(
                block => block.ToCSharp()
            ))).Indent() +
        "\n" + "}";
    }

    public string ToCSharp(string name)
    {
        return
        "public void " + name + ArgsToCSharp(true) + "\n" +
        BaseToCSharp();
    }

    public virtual string ToCSharp()
    {
        return
        ArgsToCSharp(false) + " => " + "\n" +
        BaseToCSharp();
    }

    private Var GetOrCreate(Dictionary<string, Var> table, string name)
    {
        return table.GetValueOrDefault(name) ?? (table[name] = new Var(parent: this));
    }
    private Var? Resolve(string name)
    {
        return LocalVars.GetValueOrDefault(name) ?? ParentFunction?.Resolve(name);
    }
    private Var? Declare(string name, bool isTable)
    {
        if(!Locked)
            return (LocalVars[name] = new Var(isTable, parent: this));
        else
            return ParentFunction?.Declare(name, isTable);
    }
    private Var ResolveOrDeclare(string name, bool isTable)
    {
        var v = Resolve(name) ?? Declare(name, isTable)!;
        //Debug.Assert(v.IsTable == isTable);
        return v;
    }
    public virtual Var Resolve(Reference r)
    {
        if(r.TableName != null)
        {
            Var? table = null;
            if(r.TableName == "InstanceVars")
                table = ParentScript.InstanceVars;
            else if(r.TableName == "CharVars")
                table = ParentScript.Parent.Parent.CharVars;
            else if(r.TableName == "AvatarVars")
                table = ParentScript.Parent.Parent.AvatarVars;
            else if(r.TableName == "SpellVars" && ParentScript is BBSpellScript2 ss)
                table = ss.SpellVars;
            else
            {
                table = ResolveOrDeclare(r.TableName, true);
                //table.Initialized = true; //TODO:
                //table.Used = true;
            }
            return GetOrCreate(table.Vars, r.VarName);
        }
        return ResolveOrDeclare(r.VarName, false);
    }
}