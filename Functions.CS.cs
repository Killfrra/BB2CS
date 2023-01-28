using System.Numerics;

public static class Functions_CS
{
    public static bool HasBuffOfType(
        AttackableUnit target,
        BuffType buffType
    ){
        return default!;
    }

    public static bool ExecutePeriodically(
        float timeBetweenExecutions,
        ref float trackTime,
        bool executeImmediately = false,
        float tickTime = 0
    ){
        return default!;
    }

    public static bool IsRanged(ObjAIBase ai)
    {
        return default!;
    }

    public static bool IsMelee(ObjAIBase ai)
    {
        return default!;
    }

    public static bool IsInFront(AttackableUnit me, AttackableUnit target)
    {
        return default!;
    }

    public static bool IsBehind(AttackableUnit me, AttackableUnit target)
    {
        return default!;
    }

    public static float RandomChance() // [0;1)
    {
        return default!;
    }

    public static void AddBuff(
        ObjAIBase attacker,
        AttackableUnit target,
        BBScript buffScript,
        int maxStack = 1,
        int numberOfStacks = 1,
        float duration = 25000,

        BuffAddType buffAddType = BuffAddType.REPLACE_EXISTING,
        BuffType buffType = BuffType.INTERNAL,
        float tickRate = 0,
        bool stacksExclusive = false,
        bool canMitigateDuration = false,
        bool isHiddenOnClient = false
    ){}

    //TODO: Replace all occurrences with foreach(GetUnitsInArea)+AddBuff
    public static void AddBuffToEachUnitInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        AttackableUnit buffAttacker,
        BBScript buffScript,
        BuffAddType buffAddType,
        BuffType buffType,
        int buffMaxStack,
        int buffNumberOfStacks,
        float buffDuration,
        float tickRate,
        bool isHiddenOnClient,
        bool inclusiveBuffFilter = false
    ){}

    public static IEnumerable<AttackableUnit> GetUnitsInArea(
        ObjAIBase attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    ){
        return default!;
    }
    public static IEnumerable<AttackableUnit> GetRandomUnitsInArea(
        ObjAIBase attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    ){
        return default!;
    }
    public static IEnumerable<AttackableUnit> GetClosestUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    ){
        return default!;
    }
    public static IEnumerable<AttackableUnit> GetClosestVisibleUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    ){
        return default!;
    }
    public static IEnumerable<Champion> GetChampions(
        TeamId team,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    ){
        return default!;
    }
    public static IEnumerable<AttackableUnit> GetUnitsInRectangle(
        AttackableUnit attacker,
        Vector3 center,
        float halfWidth,
        float halfLength,
        SpellDataFlags flags,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    ){
        return default!;
    }
    public static IEnumerable<AttackableUnit> GetRandomVisibleUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        [BBBuffName] string buffNameFilter,
        bool inclusiveBuffFilter
    ){
        return default!;
    }
    public static IEnumerable<Vector3> GetPointsOnLine(
        Vector3 center,
        Vector3 faceTowardsPos,
        float size,
        float pushForward,
        int iterations
    ){
        return default!;
    }
    public static IEnumerable<Vector3> GetPointsAroundCircle(
        Vector3 center,
        float radius,
        int iterations
    ){
        return default!;
    }
}