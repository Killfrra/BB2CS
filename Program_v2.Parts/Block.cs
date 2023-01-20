using System.Reflection;
using static Utils;

public class Block
{
    public string Function;
    public Dictionary<string, object> Params = new();
    public List<Block> SubBlocks = new();

    public string ResolvedName;
    public List<Union<Composite, SubBlocks, Reference>> ResolvedParams = new();
    public Reference? ResolvedReturn;

    public SubBlocks Parent;

    public void Scan(SubBlocks parent)
    {
        Parent = parent;
        ResolvedName = Function.Substring(3, Function.Length - 3 - 1);

        var flags = BindingFlags.Public | BindingFlags.Static;

        if(
            ResolvedName
            is "SetStatus" or "IncStat" or "IncPermanentStat"
            or "GetSlotSpellInfo" or "GetStat" or "GetPAROrHealth" or "GetStatus" or "GetCastInfo"
        ){
            var pInfo = typeof(Functions).GetMethod(ResolvedName, flags)!.GetParameters()[0];
            ResolvedName = (new Composite(pInfo, Params, Parent).Value as string)!;
            ResolvedName = ResolvedName.Substring(1, ResolvedName.Length - 1 - 1);
        }

        var mInfo = typeof(Functions).GetMethod(ResolvedName, flags);
        if(mInfo == null)
        {
            throw new Exception($"{ResolvedName} is undefiend");
        }

        foreach(var pInfo in mInfo.GetParameters())
        {
            var sAttr = pInfo.GetCustomAttribute<BBSubBlocks>();
            if(sAttr != null)
            {
                var sb = new SubBlocks();
                    sb.Blocks = SubBlocks;
                    sb.Scan(parent);
                foreach(var paramNameName in sAttr.ParamNames)
                {
                    var paramName = (string)Params[paramNameName + "Var"];
                    var arg = new Var(parent: sb);
                        arg.IsArgument = true;
                        arg.Initialized = true; //arg.Write(typeof(object)); //TODO:
                    sb.LocalVars[paramName] = arg;
                }
                ResolvedParams.Add(sb);
            }
            else
            {
                if(pInfo.IsOut || pInfo.ParameterType.IsByRef)
                {
                    var value = Reference.Resolve(pInfo, Params, Parent) ?? new Reference(null, "_");
                        value.IsOut = pInfo.IsOut && pInfo.ParameterType.IsByRef;
                        value.IsRef = !pInfo.IsOut && pInfo.ParameterType.IsByRef;
                    ResolvedParams.Add(value);
                }
                else
                {
                    var value = new Composite(pInfo, Params, Parent);
                    ResolvedParams.Add(value);
                }
            }

            var returnType = mInfo.ReturnType;
            if(returnType != typeof(void))
            {
                var param0 = (ResolvedParams.Count > 0) ? ResolvedParams[0].Item1 : null;
                ResolvedReturn = Reference.Resolve(mInfo, Params, Parent, param0);
            }
        }
    }

    public string ToCSharp()
    {
        //HACK:
        if(ResolvedName == nameof(Functions.SpellBuffAdd))
        {
            var c = ResolvedParams[6].Item1!;
            if(c.Var != null)
            {
                var v = Parent.Resolve(c.Var);
                if(/*!v.Initialized ||*/!v.IsTable)
                    c.Var = null;
            }
        }

        if(ResolvedName == nameof(Functions.If))
        {
            var s1 = ResolvedParams[0].Item1!.Var;
            var v1 = ResolvedParams[1].Item1!.Value;
            var cop = (CompareOp)ResolvedParams[2].Item1!.Value!;
            var s2 = ResolvedParams[3].Item1!.Var;
            var v2 = ResolvedParams[4].Item1!.Value;
            var sb = ResolvedParams[5].Item2!;

            // lazy
            string l() => (s1?.ToCSharp() ?? ((v1 != null) ? ObjectToCSharp(v1) : "default"));
            string r() => (s2?.ToCSharp() ?? ((v2 != null) ? ObjectToCSharp(v2) : "default"));
            string b() => "\n" + sb.BaseToCSharp();

            if(cop == CompareOp.CO_EQUAL)
                return $"if({l()} == {r()}){b()}";
            else if(cop == CompareOp.CO_NOT_EQUAL)
                return $"if({l()} != {r()}){b()}";
            else if(cop == CompareOp.CO_GREATER_THAN)
                return $"if({l()} > {r()}){b()}";
            else if(cop == CompareOp.CO_GREATER_THAN_OR_EQUAL)
                return $"if({l()} >= {r()}){b()}";
            else if(cop == CompareOp.CO_LESS_THAN)
                return $"if({l()} < {r()}){b()}";
            else if(cop == CompareOp.CO_LESS_THAN_OR_EQUAL)
                return $"if({l()} <= {r()}){b()}";
            
            else if(cop == CompareOp.CO_DAMAGE_SOURCETYPE_IS)
                return $"if(damage.SourceType == {l()}){b()}";
            else if(cop == CompareOp.CO_DAMAGE_SOURCETYPE_IS_NOT)
                return $"if(damage.SourceType != {l()}){b()}";

            else if(cop == CompareOp.CO_IS_TYPE_AI)
                return $"if({l()} is ObjAIBase){b()}";
            else if(cop == CompareOp.CO_IS_TYPE_HERO)
                return $"if({l()} is Champion){b()}";
            else if(cop == CompareOp.CO_IS_TYPE_TURRET)
                return $"if({l()} is BaseTurret){b()}";
            else if(cop == CompareOp.CO_IS_NOT_AI)
                return $"if({l()} is not ObjAIBase){b()}";
            else if(cop == CompareOp.CO_IS_NOT_HERO)
                return $"if({l()} is not Champion){b()}";
            else if(cop == CompareOp.CO_IS_NOT_TURRET)
                return $"if({l()} is not BaseTurret){b()}";
        }

        if(ResolvedName == nameof(Functions.SetVarInTable))
        {
            if(ResolvedReturn != null)
            {
                return ResolvedReturn.ToCSharp() + " = " + ResolvedParams[0].Item1!.ToCSharp() + ";";
            }
            return "";
        }

        return
        ((ResolvedReturn != null) ? (ResolvedReturn.ToCSharp() + " = ") : "") +
        ResolvedName + "(" +
            string.Join(", ", ResolvedParams.Select(
                p => {
                    var (composite, subBlocks, reference) = (p.Item1, p.Item2, p.Item3);
                    return composite?.ToCSharp() ?? subBlocks?.ToCSharp() ?? reference!.ToCSharp();
                }
            )) +
        ");";
    }
}