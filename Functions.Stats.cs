#nullable enable

public static partial class Functions
{
    [BBFunc]
    public static void IncPAR(AttackableUnit target, float delta/*, PrimaryAbilityResourceType PARType*/){}
    [BBFunc]
    public static void IncFlatPARPoolMod(AttackableUnit target, float delta/*, PrimaryAbilityResourceType PARType*/){}
    [BBFunc]
    public static void IncPercentPARPoolMod(AttackableUnit target, float delta/*, PrimaryAbilityResourceType PARType*/){}
    [BBFunc]
    public static void IncPermanentFlatPARPoolMod(AttackableUnit target, float delta/*, PrimaryAbilityResourceType PARType*/){}
    [BBFunc]
    public static void IncFlatPARRegenMod(AttackableUnit target, float delta/*, PrimaryAbilityResourceType PARType*/){}
    [BBFunc]
    public static void IncPercentPARRegenMod(AttackableUnit target, float delta/*, PrimaryAbilityResourceType PARType*/){}
    [BBFunc]
    public static void IncPermanentFlatPARRegenMod(AttackableUnit target, float delta/*, PrimaryAbilityResourceType PARType*/){}

    [BBFunc]
    public static void IncPermanentGoldReward(AttackableUnit target, float delta){}
    [BBFunc]
    public static void IncPermanentExpReward(AttackableUnit target, float delta){}
}