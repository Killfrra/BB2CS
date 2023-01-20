using System.Reflection;
using static Utils;

public class Reference
{
    public string? TableName;
    public string VarName;
    public bool IsOut = false;
    public bool IsRef = false;
    protected Reference(){}
    public Reference(string? tableName, string varName)
    {
        TableName = tableName;
        VarName = varName;
    }

    //TODO: Deduplicate
    public static Reference? Resolve(ParameterInfo pInfo, Dictionary<string, object> ps, SubBlocks sb)
    {
        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? new();

        var name = pInfo.Name!.UCFirst();
        var varName = (pAttr.VarPostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarTablePostfix) as string : null;
    
        if(varName != null && varName != "Nothing")
        {
            var r = new Reference(tableName, varName);
            var type = pInfo.ParameterType;
            if(pInfo.IsOut || type.IsByRef)
            {
                type = type.GetElementType();
                if(type != null && type.Name != "T") //HACK:
                {
                    var v = sb.Resolve(r);
                        v.Write(type);
                }
            }
            return r;
        }
        else
            return null;
    }

    public static Reference? Resolve(MethodInfo mInfo, Dictionary<string, object> ps, SubBlocks sb, Composite? param0)
    {
        var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>() ?? new();

        var name = fAttr.Dest.UCFirst();
        var varName = ps.GetValueOrDefault(name + "Var") as string;
        var tableName = ps.GetValueOrDefault(name + "VarTable") as string;
        if(varName != null && varName != "Nothing")
        {
            var r = new Reference(tableName, varName);
            var v = sb.Resolve(r);
            if(mInfo.Name == nameof(Functions.SetVarInTable))
                v.Assign(param0!);
            else
                v.Write(mInfo.ReturnType);
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
            output += tableName + "." + PrepareName(VarName);
        }
        else
            output += PrepareName(VarName);

        return output;
    }
}