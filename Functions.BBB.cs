#nullable enable

using System.Numerics;

public static partial class Functions
{
    [BBFunc]
    public static void If(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object/*?*/ value1,
        CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object/*?*/ src2,
        [BBParam(null, null, "", "ByLevel")]
        object/*?*/ value2,

        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static T SetVarInTable<T>(
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        T src
    ){
        return src;
    }

    [BBFunc]
    public static float Math(
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        float src1,
        object mathOp,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        float src2
    ){
        return default!;
    }

    [BBFunc]
    public static void SetStatus(
        Action<AttackableUnit, bool> status,
        AttackableUnit target,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        bool src
    ){
        status(target, src);
    }

    [BBFunc]
    public static void Else(
        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static object GetSlotSpellInfo<T>(
        Func<AttackableUnit, int, SpellbookType, SpellSlotType, T> function,
        ObjAIBase owner,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType
    ){
        return function(owner, spellSlot, spellbookType, slotType)!; // Union<string, float>
    }

    [BBFunc]
    public static void RequireVar(
        object/*?*/ required
    ){}

    [BBFunc]
    public static void IncStat(
        Action<AttackableUnit, float> stat,
        AttackableUnit target,
        float delta
    ){}

    [BBFunc]
    public static void IfHasBuff(
        AttackableUnit owner,
        AttackableUnit attacker,
        [BBBuffName] string buffName,

        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static void ElseIf(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object/*?*/ value1,
        CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object/*?*/ src2,
        [BBParam(null, null, "", "ByLevel")]
        object/*?*/ value2,

        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static float GetStat(
        Func<AttackableUnit, float> stat,
        AttackableUnit target
    ){
        return default!;
    }

    [BBFunc]
    public static void ExecutePeriodically(
        float timeBetweenExecutions,
        ref float trackTime,
        bool executeImmediately/* = false*/,
        float tickTime/* = 0*/,

        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static void BreakSpellShields(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void SetReturnValue(
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        object src
    ){}

    [BBFunc]
    public static float GetPAROrHealth(
        Func<AttackableUnit/*, PrimaryAbilityResourceType*/, float> function,
        AttackableUnit owner,
        PrimaryAbilityResourceType PARType
    ){
        return function(owner/*, PARType*/);
    }

    [BBFunc]
    public static void IfNotHasBuff(
        AttackableUnit owner,
        AttackableUnit caster,
        [BBBuffName] string buffName,

        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static void IncPermanentStat(
        Action<AttackableUnit, float> stat,
        AttackableUnit target,
        float delta
    ){}

    [BBFunc]
    public static bool GetStatus(
        Func<AttackableUnit, bool> status,
        AttackableUnit target
    ){
        return status(target);
    }

    [BBFunc]
    public static int GetBuffCountFromAll(
        AttackableUnit target,
        [BBBuffName] string/*?*/ buffName/* = null*/
    ){
        return default!;
    }

    [BBFunc]
    public static float DistanceBetweenObjects(
        object objectVar1,
        object objectVar2
    ){
        return default!;
    }

    [BBFunc]
    public static void SpellBuffRemoveStacks(
        AttackableUnit target,
        AttackableUnit attacker,
        [BBBuffName] string buffName,
        int numStacks
    ){}

    [BBFunc]
    public static T GetCastInfo<T>(
        Func<T> info
    ){
        return default!; // Union<string, float>
    }

    [BBFunc]
    public static int GetBuffCountFromCaster(
        AttackableUnit target,
        AttackableUnit/*?*/ caster,
        [BBBuffName] string buffName
    ){
        return default!;
    }

    [BBFunc]
    public static float GetTime(){
        return default!;
    }

    [BBFunc]
    public static void While(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object/*?*/ value1,
        CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object/*?*/ src2,
        [BBParam(null, null, "", "ByLevel")]
        object/*?*/ value2,

        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static void IfHasBuffOfType(
        AttackableUnit target,
        BuffType buffType,

        [BBSubBlocksAttribute]
        Action/*?*/ subBlocks/* = null*/
    ){}

    [BBFunc]
    public static void BreakExecution()
    {}

    [BBFunc]
    public static void ExecutePeriodicallyReset(
        out float trackTime
    ){
        trackTime = 0;
    }
}