using System.Numerics;
using static Utils;

//TODO: Rename
public class BBCharScript2: BBScript2
{
    protected override Type? prototype => typeof(BBCharScript);
}
//TODO: Rename
public class BBItemScript2: BBScript2
{
    protected override Type? prototype => typeof(BBItemScript);
}
//TODO: Rename
public class BBBuffScript2: BBScript2
{
    protected override Type? prototype => typeof(BBBuffScript);
    public HashSet<Var> PassedTables = new();
}
//TODO: Rename
public class BBSpellScript2: BBScript2
{
    protected override Type? prototype => typeof(BBSpellScript);
    public Var SpellVars = new(true);
}
public class BBScript2
{
    protected virtual Type? prototype => null;

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
                decl(typeof(T), name);
            }
            void decl(Type type, string name)
            {
                var v = new Var(false);
                    v.Write(type);
                    v.IsArgument = true;
                function.LocalVars[name] = v;
            }
            declare<AttackableUnit>("Owner");
            declare<AttackableUnit>("Attacker");
            declare<AttackableUnit>("Target");
            declare<int>("Slot"); // Items
            declare<int>("Level"); // ?
            declare<int>("TalentLevel"); // Talents
            declare<float>("LifeTime"); // Buffs
            /*
            if(funcName is "OnLevelUpSpell")
                declare<int>("Slot");
            if(funcName is "SelfExecute" or "TargetExecute")
            {
                declare<int>("Level");
                declare<Vector3>("TargetPos");
            }
            if(funcName is "TargetExecute" or "OnMissileUpdate")
                declare<SpellMissile>("MissileNetworkID"); //TODO: NetID or object?
            if(funcName is "SetVarsByLevel")
                declare<int>("Level");
            if(funcName is "OnHitUnit")
                declare<HitResult>("HitResult");
            if(funcName.Contains("Damage") || funcName.Contains("Hit"))
            {
                declare<DamageSource>("DamageSource");
                declare<DamageType>("DamageType");
                declare<float>("DamageAmount");
            }
            if(funcName == "OnAllowAdd")
                declare<BuffType>("Type");
            */
            var mInfo = prototype!.GetMethod(funcName)!;
            foreach(var pInfo in mInfo.GetParameters())
            {
                //TODO: Deduplicate
                var argName = pInfo.Name!.UCFirst();
                var arg = new Var(parent: function);
                    arg.Write(pInfo.ParameterType);
                    arg.IsArgument = true;
                function.LocalVars[argName] = arg;
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
                string.Join("\n", InstanceEffects.Select(
                    e => e.ToCSharpDecl()
                ).Concat(InstanceVars.Vars.Select(
                    kv => kv.Value.ToCSharp(kv.Key)
                )).Concat(Functions.Select(
                    kv => kv.Value.ToCSharp(kv.Key)
                )))).Indent() + "\n" +
            "}").Indent() + "\n" +
        "}";
    }
}