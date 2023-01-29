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

    public static Dictionary<string, HashSet<string>> Unused = new();
    public static List<Block> AllSpellBuffAdds = new();

    public void Scan(SubBlocks parent)
    {
        Parent = parent;
        ResolvedName = Function.Substring(3, Function.Length - 3 - 1);

        var used = new HashSet<string>();

        var flags = BindingFlags.Public | BindingFlags.Static;

        // Subscribing to the next scanning step
        if(ResolvedName is "SpellBuffAdd" or "ForEachUnitInTargetAreaAddBuff")
            AllSpellBuffAdds.Add(this);

        //HACK: Usually, they start with a capital letter, but sometimes they don't
        foreach(var kv in Params.ToArray())
            if(Char.IsLower(kv.Key[0]))
            {
                Params.Remove(kv.Key);
                Params.Add(kv.Key.UCFirst(), kv.Value);
            }

        var mInfo = typeof(Functions).GetMethod(ResolvedName, flags);
        if(mInfo == null)
        {
            throw new Exception($"{ResolvedName} is undefiend");
        }

        foreach(var pInfo in mInfo.GetParameters())
        {
            var sAttr = pInfo.GetCustomAttribute<BBSubBlocksAttribute>();
            if(sAttr != null)
            {
                var sb = new SubBlocks();
                
                int i = 0;
                var genericArguments = pInfo.ParameterType.GetGenericArguments();
                foreach(var paramNameName in sAttr.ParamNames)
                {
                    var argName = (string)Params.UseValueOrDefault(used, paramNameName + "Var")!;
                    var arg = new Var(parent: sb);
                        arg.Write(genericArguments[i], Parent);
                        arg.IsArgument = true;
                    sb.LocalVars[argName] = arg;
                    i++;
                }
                ResolvedParams.Add(sb);

                sb.Blocks = SubBlocks;
                sb.Scan(parent);
            }
            else
            {
                if(pInfo.IsOut || pInfo.ParameterType.IsByRef)
                {
                    var value = Reference.ResolveRefOut(pInfo, Params, used, Parent) ??
                            new Reference(null, "_", Parent); //TODO:
                        value.IsOut = pInfo.IsOut && pInfo.ParameterType.IsByRef;
                        value.IsRef = !pInfo.IsOut && pInfo.ParameterType.IsByRef;
                        value.Var.Used += Convert.ToInt32(value.IsRef);
                    ResolvedParams.Add(value);
                }
                else
                {
                    var value = new Composite(pInfo, Params, used, Parent);
                    ResolvedParams.Add(value);
                }
            }
        }

        bool isSBA = ResolvedName is nameof(Functions.SpellBuffAdd);
        if(isSBA ||  ResolvedName is nameof(Functions.ForEachUnitInTargetAreaAddBuff))
        {
            var buffName = BBName.Resolve(ResolvedParams[isSBA ? 2 : 5].Item1!.Value);
            var passedTable = ResolvedParams[isSBA ? 6 : 11].Item1!.Var!.Var;

                buffName ??= GetScript().Key;
            //  passedTable.UseAsInstanceVarsFor(GetBuffName());
            var buffScriptComposite = GetScript(buffName)?.Value;
            if(buffScriptComposite != null)
            {
                var buffScript = buffScriptComposite.BuffScript;
                //  buffScript.PassedTables.Add(passedTable);
                foreach(var kv in passedTable.Vars)
                {
                    var v = buffScript.InstanceVars.Vars.GetValueOrDefault(kv.Key);
                    if(v == null)
                        buffScript.InstanceVars.Vars[kv.Key] = v = new Var();
                    
                    bool prevInitialized = v.Initialized;
                    v.Assign(kv.Value, Parent);
                    v.Initialized = prevInitialized;
                    v.PassedFromOutside = true;

                    kv.Value.AssignTo(v, Parent);
                }
            }
        }

        if(
            ResolvedName
            is "SetStatus" or "IncStat" or "IncPermanentStat"
            or "GetSlotSpellInfo" or "GetStat" or "GetPAROrHealth" or "GetStatus" or "GetCastInfo"
        ){
            var pInfo = mInfo.GetParameters()[0];
            ResolvedName = (ResolvedParams[0].Item1!.Value as string)!;
            ResolvedName = ResolvedName.Substring(1, ResolvedName.Length - 1 - 1);
            mInfo = typeof(Functions).GetMethod(ResolvedName, flags)!;
            ResolvedParams.RemoveAt(0);
        }

        Type? returnTypeOverride = null;
        if(mInfo.GetCustomAttribute<BBBuffNameAttribute>() != null)
            returnTypeOverride = typeof(BBBuffName);
        else if(mInfo.GetCustomAttribute<BBSpellNameAttribute>() != null)
            returnTypeOverride = typeof(BBSpellName);
        /*
        if(ResolvedName is nameof(Functions.Math))
        {
            var s1 = ResolvedParams[0].Item1!;
            var op = (MathOp)ResolvedParams[1].Item1!.Value!;
            var s2 = ResolvedParams[2].Item1!;

            if(IsInteger(s1.Type!) && IsInteger(s2.Type!) && op != MathOp.MO_DIVIDE)
                returnTypeOverride = typeof(int);
        }
        */
        
        if(mInfo.ReturnType != typeof(void))
        {
            var param0 = (ResolvedParams.Count > 0) ? ResolvedParams[0].Item1 : null;
            ResolvedReturn = Reference.ResolveReturn(mInfo, Params, used, Parent, param0, returnTypeOverride);
        }

        Unused[ResolvedName] = Unused.GetValueOrDefault(ResolvedName) ?? new();
        Unused[ResolvedName].UnionWith(Params.Keys.Where(k => !used.Contains(k)));

        if(ResolvedName is "If" or "ElseIf" or "While")
        {
            ResolvedParams[1].Item1!.Var = ResolvedParams[0].Item1!.Var;
            ResolvedParams[4].Item1!.Var = ResolvedParams[3].Item1!.Var;
            ResolvedParams.RemoveAt(3);
            ResolvedParams.RemoveAt(0);

            //TODO: AssignTo(Composite)?
            var c1 = ResolvedParams[0].Item1!;
            var c2 = ResolvedParams[2].Item1!;
            if(c1.Var != null) c2.Var?.Var.AssignTo(c1.Var.Var, Parent);
            if(c1.Value != null) c2.Var?.Var.Read(c1.Value.GetType(), Parent);
            if(c2.Var != null) c1.Var?.Var.AssignTo(c2.Var.Var, Parent);
            if(c2.Value != null) c1.Var?.Var.Read(c2.Value.GetType(), Parent);
        }

        if(ResolvedName == "SetReturnValue")
        {
            //TODO: Increment ReturnValue var usage
            //TODO: Set ResolvedReturn to new Reference(null, "ReturnValue")
        }
    
        //HACK: Don't actually use it, because using this function is often false
        if(ResolvedName == nameof(Functions.RequireVar))
            ResolvedParams[0].Item1!.Var!.Var.Used--;
    }

    public void ScanSpellBuffAdd()
    {
        //HACK:
        bool isSBA = ResolvedName is nameof(Functions.SpellBuffAdd);
        if(isSBA ||  ResolvedName is nameof(Functions.ForEachUnitInTargetAreaAddBuff))
        {
            /*
            var c = ResolvedParams[6].Item1!;
            if(c.Var != null)
            {
                var v = c.Var.Var;
                if(!v.IsTable) // || !v.Initialized
                    c.Var = null;
            }
            //*/
            //*
            ResolvedName = isSBA ? "AddBuff" : "AddBuffToEachUnitInArea";
            var buffNameParam = ResolvedParams[isSBA ? 2 : 5].Item1!;
            var buffVarsTableParam = ResolvedParams[isSBA ? 6 : 11].Item1!.Var!;

            var compositeScript = Parent.ParentScript.Parent;
            var scripts = compositeScript.Parent;

            var buffName = BBName.Resolve(buffNameParam.Value);
            var buffVarsTable = Parent.Resolve(buffVarsTableParam);
            
            var buffScript = (BBBuffScript2?)null;

            if(buffName != null)
            {
                var buffScriptKV = GetScript(buffName);
                if(buffScriptKV != null)
                {
                    buffName = buffScriptKV?.Key;
                    buffScript = buffScriptKV?.Value.BuffScript;
                }
            }
            else
            {
                var currentScriptKV = GetScript();
                buffName = currentScriptKV.Key;
                buffScript = currentScriptKV.Value.BuffScript;
            }            

            var args = new List<string>();
            if(buffScript != null)
            {
                buffScript.Used = true;

                var tableRefCS = buffVarsTableParam.ToCSharp();
                foreach(var kv in buffScript.InstanceVars.Vars)
                {
                    if(kv.Value.PassedFromOutside && kv.Value.Used > 0)
                    {
                        var passed = buffVarsTable.Vars.GetValueOrDefault(kv.Key);
                        if(passed != null)
                        {
                            //HACK: Custom tables inlining
                            args.Add(PrepareName(tableRefCS + "_" + kv.Key, false));
                            //args.Add(tableRefCS + "." + PrepareName(kv.Key, true));
                            passed.UseInSubBlocks(Parent);
                            passed.Used++;
                        }
                        else
                            args.Add("default");
                    }
                }
            }
            else
            {
                //Console.WriteLine($"Could not find buff \"{buffName}\"");
                scripts.EmptyBuffScriptNames.Add(buffName);
            }

            var output =
            $"new Buffs.{PrepareName(buffName, true)}(" +
            //  ((buffVarsTable.Var.IsTable) ? buffVarsTable.ToCSharp() : "") +
                string.Join(", ", args) +
            ")";

            buffNameParam.Value = "$" + output + "$";
            
            buffVarsTable.Used--;
            ResolvedParams.RemoveAt(isSBA ? 6 : 11);
            //*/
        }
    }

    public (string Key, BBScriptComposite Value)? GetScript(string name)
    {
        var compositeScript = Parent.ParentScript.Parent;
        var kv = compositeScript.Parent.Scripts.FirstOrDefault(kv => kv.Key.ToLower() == name.ToLower());
        if(kv.Key == null)
            return null;
        return (kv.Key, kv.Value);
    }

    public (string Key, BBScriptComposite Value) GetScript()
    {
        var compositeScript = Parent.ParentScript.Parent;
        var kv = compositeScript.Parent.Scripts.First(kv => kv.Value == compositeScript);
        return (kv.Key, kv.Value);
    }

    Dictionary<int, string> int2team = new Dictionary<int, string>
    {
        {   0, "$TeamId.TEAM_UNKNOWN$" },
        { 100, "$TeamId.TEAM_BLUE$"    },
        { 200, "$TeamId.TEAM_PURPLE$"  },
        { 300, "$TeamId.TEAM_NEUTRAL$" },
    };

    string ParamsToString()
    {
        return string.Join(", ", ResolvedParams.Select(
            p => {
                var (composite, subBlocks, reference) = (p.Item1, p.Item2, p.Item3);
                return composite?.ToCSharp() ?? subBlocks?.ToCSharp() ?? reference!.ToCSharp();
            }
        ));
    }

    static Dictionary<string, string> ForEachReplacements = new()
    {
        { "ForEachUnitInTargetArea"              , "GetUnitsInArea"               },
        { "ForNClosestUnitsInTargetArea"         , "GetClosestUnitsInArea"        },
        { "ForNClosestVisibleUnitsInTargetArea"  , "GetClosestVisibleUnitsInArea" },
        { "ForEachUnitInTargetAreaRandom"        , "GetRandomUnitsInArea"         },
        { "ForEachVisibleUnitInTargetAreaRandom" , "GetRandomVisibleUnitsInArea"  },
        { "ForEachUnitInTargetRectangle"         , "GetUnitsInRectangle"          },
        { "ForEachChampion"                      , "GetChampions"                 },
        { "ForEachPointOnLine"                   , "GetPointsOnLine"              },
        { "ForEachPointAroundCircle"             , "GetPointsAroundCircle"        },
    };

    public string ToCSharp()
    {
        if(ResolvedName is "If" or "ElseIf" or "While")
        {
            var c1 = ResolvedParams[0].Item1!;
            var cop = (CompareOp)ResolvedParams[1].Item1!.Value!;
            var c2 = ResolvedParams[2].Item1!;
            var sb = ResolvedParams[3].Item2!;

            if(c1.Type == typeof(BBSpellName) && c2.Value is string s1)
                c2.Value = new BBSpellName(GetScript(s1)?.Key ?? s1);
            if(c2.Type == typeof(BBSpellName) && c1.Value is string s2)
                c1.Value = new BBSpellName(GetScript(s2)?.Key ?? s2);
            if(c1.Type == typeof(BBBuffName) && c2.Value is string s3)
                c2.Value = new BBBuffName(GetScript(s3)?.Key ?? s3);
            if(c2.Type == typeof(BBBuffName) && c1.Value is string s4)
                c1.Value = new BBBuffName(GetScript(s4)?.Key ?? s4);
            if(c1.Type == typeof(TeamId) && c2.Value != null && IsSummableType(c2.Value.GetType()))
                c2.Value = int2team[Convert.ToInt32(c2.Value)];
            if(c2.Type == typeof(TeamId) && c1.Value != null && IsSummableType(c1.Value.GetType()))
                c1.Value = int2team[Convert.ToInt32(c1.Value)];

            string n;
            if(ResolvedName == "If")
                n = "if";
            else if(ResolvedName == "ElseIf")
                n = "else if";
            else //if(ResolvedName == "While")
                n = "while";

            // lazy
            string l() => c1.ToCSharp();
            string r() => c2.ToCSharp();
            string b() => "\n" + sb.BaseToCSharp();

            if(cop == CompareOp.CO_EQUAL)
            {
                //*
                if(c1.Value is bool b1)
                    return $"{n}({(b1 ? "": "!")}{r()}){b()}";
                else if(c2.Value is bool b2)
                    return $"{n}({(b2 ? "": "!")}{l()}){b()}";
                else
                //*/
                    return $"{n}({l()} == {r()}){b()}";
            }
            else if(cop == CompareOp.CO_NOT_EQUAL)
            {
                //*
                if(c1.Value is bool b1)
                    return $"{n}({(b1 ? "!": "")}{r()}){b()}";
                else if(c2.Value is bool b2)
                    return $"{n}({(b2 ? "!": "")}{l()}){b()}";
                else
                //*/
                    return $"{n}({l()} != {r()}){b()}";
            }
            else if(cop == CompareOp.CO_GREATER_THAN)
                return $"{n}({l()} > {r()}){b()}";
            else if(cop == CompareOp.CO_GREATER_THAN_OR_EQUAL)
                return $"{n}({l()} >= {r()}){b()}";
            else if(cop == CompareOp.CO_LESS_THAN)
                return $"{n}({l()} < {r()}){b()}";
            else if(cop == CompareOp.CO_LESS_THAN_OR_EQUAL)
                return $"{n}({l()} <= {r()}){b()}";
            
            else if(cop == CompareOp.CO_DAMAGE_SOURCETYPE_IS)
                return $"{n}(damageSource == {r()}){b()}";
            else if(cop == CompareOp.CO_DAMAGE_SOURCETYPE_IS_NOT)
                return $"{n}(damageSource != {r()}){b()}";

            else if(cop == CompareOp.CO_IS_TYPE_AI)
                return $"{n}({l()} is ObjAIBase){b()}";
            else if(cop == CompareOp.CO_IS_TYPE_HERO)
                return $"{n}({l()} is Champion){b()}";
            else if(cop == CompareOp.CO_IS_TYPE_TURRET)
                return $"{n}({l()} is BaseTurret){b()}";
            else if(cop == CompareOp.CO_IS_CLONE)
                return $"{n}({l()} is Clone){b()}";
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
            ).Select( //TODO:
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
            var output = "";
            if(ResolvedReturn != null)
                output += ResolvedReturn.ToCSharp() + " = ";
            output += ResolvedParams[0].Item1!.ToCSharp() + ";";
            return output;
        }

        else if(ResolvedName == nameof(Functions.SetReturnValue))
        {
            var output = "";
            output += PrepareName("ReturnValue", false) + " = ";
            output += ResolvedParams[0].Item1!.ToCSharp() + ";";
            return output;
        }

        else if(ResolvedName == nameof(Functions.Math))
        {
            var s1 = ResolvedParams[0].Item1!;
            var op = (MathOp)ResolvedParams[1].Item1!.Value!;
            var s2 = ResolvedParams[2].Item1!;
            
            var o = "";
            if(op == MathOp.MO_ADD) o = "+";
            if(op == MathOp.MO_MULTIPLY) o = "*";
            if(op == MathOp.MO_SUBTRACT) o = "-";
            if(op == MathOp.MO_DIVIDE) o = "/";
            if(op == MathOp.MO_MODULO) o = "%";
            if(o != "")
            {
                //HACK:
                if(ResolvedReturn!.Var == s2.Var?.Var && (o == "+" || o == "*"))
                    (s1, s2) = (s2, s1);

                string s2cs() => ((o == "/" && IsInteger(s2.Type)) ? "(float)" : "") + s2.ToCSharp();

                if(ResolvedReturn!.Var == s1.Var?.Var)
                {
                    if((o == "+" || o == "-") && s2.Value != null && Convert.ToInt32(s2.Value) == 1)
                        return $"{ResolvedReturn.ToCSharp()}{o}{o};";
                    else
                        return $"{ResolvedReturn.ToCSharp()} {o}= {s2cs()};";
                }
                else
                    return $"{ResolvedReturn.ToCSharp()} = {s1.ToCSharp()} {o} {s2cs()};";
            }
            else if(op == MathOp.MO_MIN)
                return $"{ResolvedReturn!.ToCSharp()} = Math.Min({s1.ToCSharp()}, {s2.ToCSharp()});";
            else if(op == MathOp.MO_MAX)
                return $"{ResolvedReturn!.ToCSharp()} = Math.Max({s1.ToCSharp()}, {s2.ToCSharp()});";
            else if(op == MathOp.MO_ROUND)
                return $"{ResolvedReturn!.ToCSharp()} = MathF.Floor({s1.ToCSharp()});";
            else if(op == MathOp.MO_ROUNDUP)
                return $"{ResolvedReturn!.ToCSharp()} = MathF.Ceiling({s1.ToCSharp()});";
        }

        else
        {
            var replacement = ForEachReplacements.GetValueOrDefault(ResolvedName);
            if(replacement != null)
            {
                int i = ResolvedParams.Count - 1;
                SubBlocks? sb = ResolvedParams[i].Item2!;
                ResolvedParams.RemoveAt(i);
                var iterKV = sb.LocalVars.First(v => v.Value.IsArgument);
                var iter = iterKV.Value.ToCSharpArg(iterKV.Key, true);
                return $"foreach({iter} in {replacement}({ParamsToString()}))\n" + sb.BaseToCSharp();
            }
        }

        var comment = "";
        if(ResolvedName == "RequireVar")
            comment = "//";

        return
        comment +
        ((ResolvedReturn != null) ? (ResolvedReturn.ToCSharp() + " = ") : "") +
        ResolvedName + "(" + ParamsToString() + ");";
    }
}