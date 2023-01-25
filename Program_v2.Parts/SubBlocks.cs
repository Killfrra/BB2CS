using static Utils;

//TODO: Deprecate
public class BBFunction: SubBlocks
{
    //public string Name;
    public override bool Locked => false;

    public Var? Return;
    public object? DefaultReturn;

    public string ToCSharp(string name)
    {
        var type = (Return != null) ? Return.Type : typeof(void);
        return
        "public override " + TypeToCSharp(type) + " " + name + ArgsToCSharp(true) + "\n" +
        BaseToCSharp();
    }
}

public class SubBlocks
{
    public List<Block> Blocks = new();
    public Dictionary<string, Var> LocalVars = new();
    public virtual bool Locked => true;
    
    public BBScript2 ParentScript;
    public SubBlocks? ParentBlock;
    //public BBFunction? ParentFunction;

    public virtual void Scan(SubBlocks parentBlock)
    {
        Scan(parentBlock.ParentScript, parentBlock);
    }
    public void Scan(BBScript2 parentScript, SubBlocks? parentBlock = null)
    {
        ParentScript = parentScript;
        ParentBlock = parentBlock;
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
        var func = this as BBFunction;
        bool shouldReturn = func != null && func.Return != null;
        
        var body = string.Join("\n", LocalVars.Where(
            kv => !(
                kv.Value.IsArgument
                || kv.Key == "_" // HACK:
                || (shouldReturn && kv.Key == "ReturnValue")
                || (kv.Value.Used == 0 && (
                    kv.Value.PassedFromOutside
                    || !kv.Value.Initialized)))
        ).Select(
            kv => kv.Value.ToCSharp(kv.Key, false, true)
        ).Concat(Blocks.Select(
            block => block.ToCSharp()
        )));

        if(shouldReturn)
        {
            body = body.Trim();
            body =
            func!.Return!.BaseToCSharp("ReturnValue", false, false) + " = " +
            ObjectToCSharp(func.DefaultReturn) + ";\n" +
            body + ((body != "") ? "\n" : "") +
            "return " + PrepareName("ReturnValue", false) + ";";
        }

        return Braces(body);
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
        return LocalVars.GetValueOrDefault(name) ?? (
            (ParentBlock != null) ?
                ParentBlock.Resolve(name)
                : ParentScript.Resolve(name)
        );
    }
    private Var? Declare(string name, bool isTable)
    {
        if(!Locked)
            return (LocalVars[name] = new Var(isTable, parent: this));
        else
            return ParentBlock?.Declare(name, isTable);
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
                table.IsCustomTable = true;
                table.Initialized = true; //TODO:
                table.Used++;
            }
            return GetOrCreate(table.Vars, r.VarName);
        }
        return ResolveOrDeclare(r.VarName, false);
    }
}