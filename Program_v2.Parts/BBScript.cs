using System.Numerics;
using static Utils;

//TODO: Rename
public class BBSpellScript2: BBScript
{
    public Var SpellVars = new(true);
}
public class BBScript
{
    public Dictionary<string, object> Metadata = new();
    public Dictionary<string, BBFunction> Functions = new();
    public Var InstanceVars = new(true);
    public List<EffectReference> InstanceEffects = new();

    public BBScriptComposite Parent;

    public void Scan(BBScriptComposite parent)
    {
        Parent = parent;
        foreach(var kv in Functions)
        {
            var function = kv.Value;
            var funcName = kv.Key;

            //HACK:
            void declare<T>(string name)
            {
                var v = new Var(false);
                    v.Write(typeof(T));
                    v.IsArgument = true;
                function.LocalVars[name] = v;
            }
            declare<AttackableUnit>("Owner");
            declare<AttackableUnit>("Attacker");
            declare<AttackableUnit>("Target");
            if(funcName is "OnLevelUpSpell")
                declare<int>("Slot");
            if(funcName is "SelfExecute" or "TargetExecute")
            {
                declare<int>("Level");
                declare<Vector3>("TargetPos");
            }
            if(funcName is "TargetExecute" or "OnMissileUpdate")
                declare<SpellMissile>("missileNetworkID"); //TODO: NetID or object?
            if(funcName is "SetVarsByLevel")
                declare<int>("Level");
            if(funcName is "OnHitUnit")
                declare<HitResult>("HitResult");
            if(funcName.Contains("Damage") || funcName.Contains("Hit"))
            {
                declare<DamageSource>("damageSource");
                declare<DamageType>("damageType");
            }

            function.Scan(this, null);
        }
    }

    public string ToCSharp(string ns, string name)
    {
        var output = "";
        foreach(var func in Functions) //TODO: MultiSelect?
        {
            foreach(var v in func.Value.LocalVars)
            {
                if(v.Value.IsTable)
                {
                    output +=
                    "class " + func.Key + "_" + PrepareName(v.Key, false) + "\n" +
                    "{" + "\n" +
                        string.Join("\n", v.Value.Vars.Select(
                            kv => kv.Value.ToCSharp(kv.Key, true)
                        )).Indent() + "\n" +
                    "}" + "\n";
                }
            }
        }

        return
        "namespace " + ns + "\n" +
        "{" + "\n" + (
            "public class " + PrepareName(name, true) + " : Script" + "\n" +
            "{" + "\n" + (
                output +
                string.Join("", InstanceEffects.Select(
                    e => e.ToCSharpDecl() + "\n"
                )) +
                string.Join("", InstanceVars.Vars.Select(
                    kv => kv.Value.ToCSharp(kv.Key) + "\n"
                )) +
                string.Join("\n", Functions.Select(
                    kv => kv.Value.ToCSharp(kv.Key)
                ))).Indent() + "\n" +
            "}").Indent() + "\n" +
        "}";
    }
}