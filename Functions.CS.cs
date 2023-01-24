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
        bool executeImmediately = false
        //TODO: ref float tickTime?
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
    //*
    public static void AddBuff(
        ObjAIBase attacker,
        AttackableUnit target,
        Script buffScript,
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
    //*/
}