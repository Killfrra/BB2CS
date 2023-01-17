[Flags]
public enum SpellDataFlags //: uint
{
    AutoCast = 1 << 1,
    InstantCast = 1 << 2,
    InstaCast = 1 << 2, //HACK:
    Instacast = 1 << 2, //HACK:
    PersistThroughDeath = 1 << 3,
    NonDispellable = 1 << 4,
    NoClick = 1 << 5,
    AffectImportantBotTargets = 1 << 6,
    AllowWhileTaunted = 1 << 7,
    NotAffectZombie = 1 << 8,
    NotAffectZombies = 1 << 8, //HACK:
    AffectUntargetable = 1 << 9,
    AffectEnemies = 1 << 10,
    AffectFriends = 1 << 11,
    AffectFriendly = 1 << 11, //HACK:
    AffectNeutral = 1 << 14,
    AffectNeutrals = 1 << 14, //HACK:
    AffectAllSides = AffectEnemies + AffectFriends + AffectNeutral,
    AffectBuildings = 1 << 12,
    AffectMinions = 1 << 15,
    AffectHeroes = 1 << 16,
    AffectTurrets = 1 << 17,
    AffectAllUnitTypes = AffectMinions + AffectHeroes + AffectTurrets,
    NotAffectSelf = 1 << 13,
    AlwaysSelf = 1 << 18,
    AffectAlwaysSelf = 1 << 18, //HACK:
    AffectDead = 1 << 19,
    AffectNotPet = 1 << 20,
    AffectBarracksOnly = 1 << 21, //TODO: Remove?
    AffectBarrackOnly = 1 << 21, //HACK:
    IgnoreVisibilityCheck = 1 << 22,
    NonTargetableAlly = 1 << 23,
    NonTargetableEnemy = 1 << 24,
    NonTargetableAll = NonTargetableAlly + NonTargetableEnemy,
    TargetableToAll = 1 << 25,
    AffectWards = 1 << 26,
    AffectsWards = 1 << 26, //HACK:
    AffectUseable = 1 << 27,
    IgnoreAllyMinion = 1 << 28,
    IgnoreEnemyMinion = 1 << 29,
    IgnoreLaneMinion = 1 << 30,
    IgnoreClones = 1 << 31,
}