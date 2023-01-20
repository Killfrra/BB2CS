using System.Reflection;
using Newtonsoft.Json.Linq;
using static Utils;

public class Composite
{
    public Type? Type;
    public object? Value;
    public Reference? Var;
    public EffectReference? VarByLevel;

    public Composite(ParameterInfo pInfo, Dictionary<string, object> ps, SubBlocks sb)
    {
        var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? new();

        var name = pInfo.Name!.UCFirst();
        var value = (pAttr.ValuePostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValuePostfix) : null;
        var valueByLevel = (pAttr.ValueByLevelPostfix != null) ? ps.GetValueOrDefault(name + pAttr.ValueByLevelPostfix) : null;
        var varName = (pAttr.VarPostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarPostfix) as string : null;
        var tableName = (pAttr.VarTablePostfix != null) ? ps.GetValueOrDefault(name + pAttr.VarTablePostfix) as string : null;

        Type = pInfo.ParameterType;
        if(Type.Name == "T") //HACK:
            Type = null;

        Value = AskConstants(value);

        if(varName != null && varName != "Nothing")
        {
            Var = new Reference(tableName, varName);
            var v = sb.Resolve(Var);
            v.Used = true;
        }
        if(valueByLevel != null)
        {
            var arr = ((JArray)valueByLevel).ToObject<object[]>()!;
            var id = sb.ParentScript.InstanceEffects.Count;
            VarByLevel = new EffectReference(id, arr);
            sb.ParentScript.InstanceEffects.Add(VarByLevel);
        }
    }

    public string ToCSharp()
    {
        int count = Convert.ToInt32(Value != null) +
                    Convert.ToInt32(Var != null) +
                    Convert.ToInt32(VarByLevel != null);
        if(count > 0)
        {
            if(count == 1)
            {
                if(Value != null)
                {
                    return ObjectToCSharp(Value);
                }
                else
                {
                    var r = (Var ?? VarByLevel)!;
                    return r.ToCSharp();
                }
            }
            else if(IsSummableType(Type))
            {
                var output = new List<string>();
                if(Value != null && Convert.ToSingle(Value) != 0)
                {
                    output.Add(ObjectToCSharp(Value));
                }
                if(Var != null)
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
                if(Var != null)
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

    object? AskConstants(object? value)
    {
        if(value is string s && s.StartsWith("$") && s.EndsWith("$"))
        {
            s = s.Substring(1, s.Length - 1 - 1);
            var constant = Constants.Table.GetValueOrDefault(s);
            //if(constant == null)
            //    Console.WriteLine($"Failed to resolve constant {s}");
            return constant ?? value;
        }
        return value;
    }

    public void InferType()
    {
        if(Type != null)
            return;
        List<Type> types = new();
        if(Value != null)
            types.Add(Value.GetType());
        if(VarByLevel != null && VarByLevel.Type != null)
            types.Add(VarByLevel.Type);
        
        //TODO: Var.Var.InferType
        
        Type = InferTypeFrom(types);
    }
}