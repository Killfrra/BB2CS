using System.Reflection;
using static Utils;

public class Reference
{
    SubBlocks Block; //TODO: Rename?
    public string? TableName;
    public string VarName;
    public Var Var;
    public bool IsOut = false;
    public bool IsRef = false;

    protected Reference(){}
    public Reference(string? tableName, string varName, SubBlocks sb)
    {
        Block = sb;
        TableName = tableName;
        VarName = varName;
        Var = sb.Resolve(this);
    }

    //TODO: Deduplicate
    public static Reference? ResolveRefOut(ParameterInfo pInfo, Dictionary<string, object> ps, HashSet<string> used, SubBlocks sb)
    {
        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? new();

        var name = pInfo.Name!.UCFirst();
        var varName = (pAttr.VarPostfix != null) ? ps.UseValueOrDefault(used, name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.UseValueOrDefault(used, name + pAttr.VarTablePostfix) as string : null;
    
        if(varName != null && varName != "Nothing")
        {
            var r = new Reference(tableName, varName, sb);
            var type = pInfo.ParameterType;
            if(pInfo.IsOut || type.IsByRef)
                type = type.GetElementType();
            if(type != null && type.Name != "T") //HACK:
                r.Var.Write(type, sb);
            return r;
        }
        else
            return null;
    }

    public static Reference? ResolveReturn(MethodInfo mInfo, Dictionary<string, object> ps, HashSet<string> used, SubBlocks sb, Composite? param0, Type? returnTypeOverride)
    {
        var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>() ?? new();

        var name = fAttr.Dest;
        var varName = ps.UseValueOrDefault(used, name + "Var") as string;
        var tableName = ps.UseValueOrDefault(used, name + "VarTable") as string;
        if(varName != null && varName != "Nothing")
        {
            var r = new Reference(tableName, varName, sb);
            if(mInfo.Name == nameof(Functions.SetVarInTable))
            {
                r.Var.Assign(param0!, sb);
                param0!.Var?.Var.AssignTo(r.Var, sb);
            }
            else
                r.Var.Write(returnTypeOverride ?? mInfo.ReturnType, sb);
            return r;
        }
        else
            return null;
    }

    public virtual string ToCSharp()
    {
        var output = "";

        if(IsOut) output += "out ";
        if(IsRef) output += "ref ";

        if(TableName != null)
        {
            var tableName = TableName;
            if(tableName == "InstanceVars")
                tableName = "this";
            //HACK: Custom tables inlining
            if(!(TableName is "InstanceVars" or "CharVars" or "AvatarVars" or "SpellVars"))
                output += PrepareName(tableName + "_" + VarName, false);
            else
                output += PrepareName(tableName, false) + "." + PrepareName(VarName, tableName != "this");
        }
        else
            output += PrepareName(VarName, false);

        return output;
    }
}