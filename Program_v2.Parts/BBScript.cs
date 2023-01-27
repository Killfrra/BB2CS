using System.Numerics;
using System.Reflection;
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
    public BuffScriptMetadataUnmutable MetaData = new();
    public override string MetaDataToCSharp() => MetaData.ToCSharp();
}
//TODO: Rename
public class BBSpellScript2: BBScript2
{
    protected override Type? prototype => typeof(BBSpellScript);
    public Var SpellVars = new(true);
    public SpellScriptMetaDataNullable MetaData = new();
    public override string MetaDataToCSharp() => MetaData.ToCSharp();
}
public class BBScript2
{
    protected virtual Type? prototype => null;

    //public Dictionary<string, object> Metadata = new();
    public Dictionary<string, BBFunction> Functions = new();
    public Var InstanceVars = new(true);
    public List<EffectReference> InstanceEffects = new();

    public BBScriptComposite Parent;

    public bool Used = false;

    public BBScript2()
    {
        var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        foreach(var fInfo in prototype!.GetFields(flags))
        {
            var v = new Var();
                v.IsArgument = true;
                v.Initialized = true;
                v.Type = fInfo.FieldType;
            InheritedVariables.Add(fInfo.Name.UCFirst(), v);
        }
    }

    public virtual string MetaDataToCSharp()
    {
        return "";
    }

    public void Scan(BBScriptComposite parent)
    {
        Parent = parent;
        foreach(var kv in Functions)
        {
            var function = kv.Value;
            var funcName = kv.Key;

            /*
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
            declare<  ObjAIBase   >("Attacker");
            declare<AttackableUnit>("Target");
            declare<int>("Slot"); // Items
            declare<int>("Level"); // ?
            declare<int>("TalentLevel"); // Talents
            declare<float>("LifeTime"); // Buffs
            */
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
            //TODO: Move to Function
            var mInfo = prototype!.GetMethod(funcName)!;
            foreach(var pInfo in mInfo.GetParameters())
            {
                //TODO: Deduplicate
                var argName = pInfo.Name!.UCFirst();
                var arg = new Var(parent: function);
                    arg.Write(GetParamType(pInfo));
                    arg.IsArgument = true;
                function.LocalVars[argName] = arg;
            }
            if(mInfo.ReturnType != typeof(void))
            {
                var cAttr = mInfo.GetCustomAttribute<BBCallAttribute>();
                var v = new Var(parent: function);
                    v.Type = mInfo.ReturnType;
                    v.Initialized = true;
                function.Return = v;
                function.DefaultReturn = cAttr?.DefaultReturnValue;
                function.LocalVars["ReturnValue"] = v;
            }
            function.Scan(this, null);
        }
    }

    public string ToCSharp(string ns, string name)
    {
        var outsiders = InstanceVars.Vars.Where(kv => kv.Value.PassedFromOutside && kv.Value.Used > 0);
        var constructor = (outsiders.Count() == 0) ? "" :
        $"public {PrepareName(name, true)}(" + 
            string.Join(", ", outsiders.Select(v => v.Value.BaseToCSharp(v.Key, false, true))) +
        ")\n{\n" +
            string.Join("\n", outsiders.Select(
                v => $"this.{PrepareName(v.Key, false)} = {PrepareName(v.Key, false)};"
            )).Indent() +
        "\n}\n";

        return
        "namespace " + ns + "\n" +
        Braces(
            "public class " + PrepareName(name, true) + " : " + prototype!.Name + "\n" +
            Braces(
                MetaDataToCSharp() +
                string.Join("", InstanceVars.Vars.Where(
                    kv => kv.Value.Used > 0 || kv.Value.Initialized
                ).Select(
                    kv => kv.Value.ToCSharp(kv.Key, false, !outsiders.Contains(kv)) + "\n"
                ).Concat(InstanceEffects.Select(
                    e => e.ToCSharpDecl() + "\n"
                ))) +
                constructor + 
                string.Join("\n", Functions.Where(
                    func => func.Value.Blocks.Count > 0
                ).Select(
                    func =>
                    string.Join("", func.Value.LocalVars.Where(
                        v => v.Value.IsTable && !v.Value.IsArgument
                    ).Select(
                        v =>
                        "class " + func.Key + "_" + PrepareName(v.Key, false) + "\n" +
                        Braces(
                            string.Join("\n", v.Value.Vars.Select(
                                kv => kv.Value.ToCSharp(kv.Key, true, true)
                            ))
                        ) + "\n"
                    )) +
                    func.Value.ToCSharp(func.Key)
                ))
            )
        );
    }

    public Dictionary<string, Var> InheritedVariables = new();
    public Var? Resolve(string name)
    {
        return InheritedVariables.GetValueOrDefault(name);
    }
}