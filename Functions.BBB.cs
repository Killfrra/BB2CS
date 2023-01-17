#nullable enable

using System.Numerics;

public static partial class Functions
{
    [BBFunc]
    public static void If(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object? value1,
        CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object? src2,
        [BBParam(null, null, "", "ByLevel")]
        object? value2,

        [BBSubBlocks]
        Action subBlocks
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
        bool src
    ){
        status(target, src);
    }

    [BBFunc]
    public static void Else(
        [BBSubBlocks]
        Action subBlocks
    ){}

    [BBFunc]
    public static object GetSlotSpellInfo<T>(
        Func<AttackableUnit, int, SpellbookType, SpellSlotType, T> function,
        AttackableUnit owner,
        int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType
    ){
        return function(owner, spellSlot, spellbookType, slotType)!; // Union<string, float>
    }

    [BBFunc]
    public static void RequireVar(
        object? required
    ){}

    [BBFunc]
    public static void IncStat(
        object stat,
        AttackableUnit target,
        float delta
    ){}

    [BBFunc]
    public static void IfHasBuff(
        AttackableUnit owner,
        AttackableUnit attacker,
        string buffName,

        [BBSubBlocks]
        Action subBlocks
    ){}

    [BBFunc]
    public static void ElseIf(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object? value1,
        CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object? src2,
        [BBParam(null, null, "", "ByLevel")]
        object? value2,

        [BBSubBlocks]
        Action subBlocks
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
        bool executeImmediately = false,
        //TODO: ref float tickTime?

        [BBSubBlocks]
        Action? subBlocks = null
    ){}

    [BBFunc]
    public static void BreakSpellShields(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void SetReturnValue(
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
        string buffName,

        [BBSubBlocks]
        Action? subBlocks = null
    ){}

    [BBFunc]
    public static void IncPermanentStat(
        object stat,
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
        string? buffName
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
        string buffName,
        int numStacks
    ){}

    [BBFunc]
    public static object GetCastInfo(
        object info
    ){
        return default!; // Union<string, float>
    }

    [BBFunc]
    public static int GetBuffCountFromCaster(
        AttackableUnit target,
        AttackableUnit caster,
        string buffName
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
        object? value1,
        CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object? src2,
        [BBParam(null, null, "", "ByLevel")]
        object? value2,

        [BBSubBlocks]
        Action subBlocks
    ){}

    [BBFunc]
    public static void IfHasBuffOfType(
        AttackableUnit target,
        BuffType buffType,

        [BBSubBlocks]
        Action subBlocks
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