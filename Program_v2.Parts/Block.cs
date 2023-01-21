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
                int i = 0;
                var genericArguments = pInfo.ParameterType.GetGenericArguments();
                foreach(var paramNameName in sAttr.ParamNames)
                {
                    var paramName = (string)Params[paramNameName + "Var"];
                    var arg = new Var(parent: sb);
                        arg.Write(genericArguments[i]);
                        arg.IsArgument = true;
                    sb.LocalVars[paramName] = arg;
                    i++;
                }
                ResolvedParams.Add(sb);
            }
            else
            {
                if(pInfo.IsOut || pInfo.ParameterType.IsByRef)
                {
                    var value = Reference.Resolve(pInfo, Params, Parent) ??
                            new Reference(null, "_", Parent); //TODO:
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
        }

        var returnType = mInfo.ReturnType;
        if(returnType != typeof(void))
        {
            var param0 = (ResolvedParams.Count > 0) ? ResolvedParams[0].Item1 : null;
            ResolvedReturn = Reference.Resolve(mInfo, Params, Parent, param0);
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
                var v = c.Var.Var;
                if(/*!v.Initialized ||*/!v.IsTable)
                    c.Var = null;
            }
        }

        else if(ResolvedName is "If" or "ElseIf" or "While")
        {
            var s1 = ResolvedParams[0].Item1!.Var;
            var v1 = ResolvedParams[1].Item1!;
            var cop = (CompareOp)ResolvedParams[2].Item1!.Value!;
            var s2 = ResolvedParams[3].Item1!.Var;
            var v2 = ResolvedParams[4].Item1!;
            var sb = ResolvedParams[5].Item2!;

            string n;
            if(ResolvedName == "If")
                n = "if";
            else if(ResolvedName == "ElseIf")
                n = "else if";
            else //if(ResolvedName == "While")
                n = "while";

            // lazy
            string l() => s1?.ToCSharp() ?? v1.ToCSharp();
            string r() => s2?.ToCSharp() ?? v2.ToCSharp();
            string b() => "\n" + sb.BaseToCSharp();

            if(cop == CompareOp.CO_EQUAL)
                return $"{n}({l()} == {r()}){b()}";
            else if(cop == CompareOp.CO_NOT_EQUAL)
                return $"{n}({l()} != {r()}){b()}";
            else if(cop == CompareOp.CO_GREATER_THAN)
                return $"{n}({l()} > {r()}){b()}";
            else if(cop == CompareOp.CO_GREATER_THAN_OR_EQUAL)
                return $"{n}({l()} >= {r()}){b()}";
            else if(cop == CompareOp.CO_LESS_THAN)
                return $"{n}({l()} < {r()}){b()}";
            else if(cop == CompareOp.CO_LESS_THAN_OR_EQUAL)
                return $"{n}({l()} <= {r()}){b()}";
            
            else if(cop == CompareOp.CO_DAMAGE_SOURCETYPE_IS)
                return $"{n}(damage.SourceType == {l()}){b()}";
            else if(cop == CompareOp.CO_DAMAGE_SOURCETYPE_IS_NOT)
                return $"{n}(damage.SourceType != {l()}){b()}";

            else if(cop == CompareOp.CO_IS_TYPE_AI)
                return $"{n}({l()} is ObjAIBase){b()}";
            else if(cop == CompareOp.CO_IS_TYPE_HERO)
                return $"{n}({l()} is Champion){b()}";
            else if(cop == CompareOp.CO_IS_TYPE_TURRET)
                return $"{n}({l()} is BaseTurret){b()}";
            else if(cop == CompareOp.CO_IS_NOT_AI)
                return $"{n}({l()} is not ObjAIBase){b()}";
            else if(cop == CompareOp.CO_IS_NOT_HERO)
                return $"{n}({l()} is not Champion){b()}";
            else if(cop == CompareOp.CO_IS_NOT_TURRET)
                return $"{n}({l()} is not BaseTurret){b()}";

            else if(cop == CompareOp.CO_IS_DEAD)
                return $"{n}({l()}.IsDead){b()}";
            else if(cop == CompareOp.CO_IS_NOT_DEAD)
                return $"{n}(!{l()}.IsDead){b()}";
            
            else if(cop == CompareOp.CO_SAME_TEAM)
                return $"{n}({l()}.Team == {r()}.Team){b()}";
            else if(cop == CompareOp.CO_DIFFERENT_TEAM)
                return $"{n}({l()}.Team != {r()}.Team){b()}";

            else if(cop == CompareOp.CO_IS_RANGED)
                return $"{n}(IsRanged({l()})){b()}";
            else if(cop == CompareOp.CO_IS_MELEE)
                return $"{n}(IsMelee({l()})){b()}";

            else if(cop == CompareOp.CO_IS_TARGET_IN_FRONT_OF_ME)
                return $"{n}(IsInFront({l()}, {r()})){b()}";
            else if(cop == CompareOp.CO_IS_TARGET_BEHIND_ME)
                return $"{n}(IsBehind({l()}, {r()})){b()}";

            else if(cop == CompareOp.CO_RANDOM_CHANCE_LESS_THAN)
                return $"{n}(RandomChance() < {l()}){b()}";
        }

        else if(ResolvedName == "Else")
        {
            var sb = ResolvedParams.Last().Item2!;
            return "else\n" + sb.BaseToCSharp();
        }

        else if(ResolvedName is "IfHasBuff" or "IfNotHasBuff" or "IfHasBuffOfType" or "ExecutePeriodically")
        {
            var sb = ResolvedParams.Last().Item2!;

            var b = "\n" + sb.BaseToCSharp();
            var ps = string.Join(", ", ResolvedParams.Where(
                p => p.Item2 != sb
            ).Select(
                p => p.Item1?.ToCSharp() ?? p.Item2?.ToCSharp() ?? p.Item3!.ToCSharp()
            ));

            if(ResolvedName == "IfHasBuff")
                return $"if(GetBuffCountFromCaster({ps}) > 0){b}";
            else if(ResolvedName == "IfNotHasBuff")
                return $"if(GetBuffCountFromCaster({ps}) == 0){b}";
            else if(ResolvedName == "IfHasBuffOfType")
                return $"if(HasBuffOfType({ps})){b}";
            else if(ResolvedName == "ExecutePeriodically")
                return $"if(ExecutePeriodically({ps})){b}";
        }

        else if(ResolvedName == nameof(Functions.SetVarInTable))
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