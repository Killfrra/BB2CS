#nullable enable

using System;

[AttributeUsage(AttributeTargets.Parameter)]
public class BBParamAttribute : Attribute
{
    public string? VarPostfix = "Var";
    public string? VarTablePostfix = "VarTable";
    public string? ValuePostfix = "";
    public string? ValueByLevelPostfix = "ByLevel";
    public BBParamAttribute()
    {
    }
    public BBParamAttribute(
        string? varPostfix,
        string? varTablePostfix,
        string? valuePostfix,
        string? valueByLevelPostfix
    )
    {
        VarPostfix = varPostfix;
        VarTablePostfix = varTablePostfix;
        ValuePostfix = valuePostfix;
        ValueByLevelPostfix = valueByLevelPostfix;
    }
}

[AttributeUsage(AttributeTargets.Parameter)]
public class BBSubBlocks : Attribute
{
    public string[] ParamNames;
    public BBSubBlocks(params string[] paramNames)
    {
        ParamNames = paramNames;
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class BBFuncAttribute : Attribute
{
    public string Dest = "Dest";
}

[AttributeUsage(AttributeTargets.Method)]
public class BBCallAttribute : Attribute
{
    public string Name;
    public BBCallAttribute(string name)
    {
        Name = name;
    }
}