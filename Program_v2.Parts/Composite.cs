using System.Numerics;
using System.Reflection;
using Newtonsoft.Json.Linq;
using static Utils;

public class Composite
{
    public Type? Type;
    public object? Value;
    public Reference? Var;
    public EffectReference? VarByLevel;

    public static List<Composite> All = new();

    //TODO: Deduplicate
    public Composite(ParameterInfo pInfo, Dictionary<string, object> ps, HashSet<string> used, SubBlocks sb)
    {
        All.Add(this);

        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? new();

        var name = pInfo.Name!.UCFirst();
        var value = (pAttr.ValuePostfix != null) ? ps.UseValueOrDefault(used, name + pAttr.ValuePostfix) : null;
        var valueByLevel = (pAttr.ValueByLevelPostfix != null) ? ps.UseValueOrDefault(used, name + pAttr.ValueByLevelPostfix) : null;
        var varName = (pAttr.VarPostfix != null) ? ps.UseValueOrDefault(used, name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.UseValueOrDefault(used, name + pAttr.VarTablePostfix) as string : null;

        Type = Nullable.GetUnderlyingType(pInfo.ParameterType) ?? pInfo.ParameterType;
        if(Type.Name == "T" || Type == typeof(object)) //HACK:
            Type = null;

        Value = AskConstants(value);

        var b = sb.Blocks[0]; //HACK:
        var scripts = sb.ParentScript.Parent.Parent;
        if(pInfo.GetCustomAttribute<BBBuffNameAttribute>() != null)
        {
            Type = typeof(BBBuffName);
            if(Value is string s)
            {
                var script = b.GetScript(s);
                var buffScript = script?.Value.BuffScript;
                if(buffScript != null)
                    buffScript.Used = true;
                else
                    scripts.EmptyBuffScriptNames.Add(s);
                Value = new BBBuffName(script?.Key ?? s);
            }
        }
        else if(pInfo.GetCustomAttribute<BBSpellNameAttribute>() != null)
        {
            Type = typeof(BBSpellName);
            if(Value is string s)
            {
                var script = b.GetScript(s);
                var spellScript = script?.Value.SpellScript;
                if(spellScript != null)
                    spellScript.Used = true;
                Value = new BBSpellName(script?.Key ?? s);
            }
        }

        if(varName != null && varName != "Nothing")
        {
            Var = new Reference(tableName, varName, sb);
            Var.Var.Used++;

            if(Type != null)
                Var.Var.Read(Type, sb);
        }
        if(valueByLevel != null)
        {
            var arr = ((JArray)valueByLevel).ToObject<object[]>()!;
            var id = sb.ParentScript.InstanceEffects.Count;

            VarByLevel = new EffectReference(id, arr, sb);
            VarByLevel.Var.Used++;
            
            sb.ParentScript.InstanceEffects.Add(VarByLevel);
        }
    }

    public string ToCSharp()
    {
        //InferType();

        int count = Convert.ToInt32(Value != null) +
                    Convert.ToInt32(Var != null/* && Var.Var.Initialized*/) +
                    Convert.ToInt32(VarByLevel != null);
        if(count > 0)
        {
            if(count == 1)
            {
                if(Value != null)
                {
                    if(Type == typeof(SpellDataFlags) && Value is string s)
                    {
                        var opt = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                        return string.Join(" | ", s.Split(" ", opt).Select(
                            f => "SpellDataFlags" + "." + f
                        ));
                    }
                    return ObjectToCSharp(Value);
                }
                else if(VarByLevel != null)
                {
                    return VarByLevel.ToCSharp();
                }
                else //if(Var != null)
                {
                    var r = Var!;
                    var v = Var!.Var;
                    //  v.InferType();
                    if(Type != null && v.Type != null && v.Type != Type)
                    {
                        if(Type.IsAssignableTo(v.Type))
                            return "(" + TypeToCSharp(Type) + ")" + r.ToCSharp();

                        if(
                            (Type == typeof(Vector3) || Type == typeof(Vector3?)) //TODO: Nullables
                            && v.Type.IsAssignableTo(typeof(GameObject))
                        )
                            return r.ToCSharp() + ".Position";
                    }
                    return r.ToCSharp();
                }
            }
            else if(Type != null && IsSummableType(Type))
            {
                var output = new List<string>();
                if(Value != null && Convert.ToSingle(Value) != 0)
                {
                    output.Add(ObjectToCSharp(Value));
                }
                if(Var != null/* && Var.Var.Initialized*/)
                {
                    output.Add(Var.ToCSharp());
                }
                if(VarByLevel != null)
                {
                    output.Add(VarByLevel.ToCSharp());
                }
                return string.Join(" + ", output);
            }
            else
            {
                var output = new List<string>();
                if(Var != null/* && Var.Var.Initialized*/)
                {
                    output.Add(Var.ToCSharp());
                }
                if(VarByLevel != null)
                {
                    output.Add(VarByLevel.ToCSharp());
                }
                else if(Value != null)
                {
                    output.Add(ObjectToCSharp(Value));
                }
                return string.Join(" ?? ", output);
            }
        }
        else
            return "default";
    }

    bool inferred = false;
    public void InferType()
    {
        if(inferred || Type != null)
        {
            //inferred = true;
            return;
        }
        List<Type> types = new();
        if(Value != null)
            types.Add(Value.GetType());
        if(VarByLevel != null && VarByLevel.Type != null)
            types.Add(VarByLevel.Type);
        
        if(Var != null)
        {
            //Var.Var.InferType();
            if(Var.Var.Type != null)
                types.Add(Var.Var.Type);
        }
        
        Type = InferTypeFrom(types);
        //inferred = true;
    }
}