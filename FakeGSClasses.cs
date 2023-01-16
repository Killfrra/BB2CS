public class AttackableUnit {}
public class ObjAIBase: AttackableUnit {}
public class Minion: ObjAIBase {}
public enum TeamId
{
    TEAM_UNKNOWN,
}
public class Champion: ObjAIBase {}
public enum BuffAddType
{
    REPLACE_EXISTING,
}
public enum BuffType
{
    INTERNAL,
}
public class Spell {}
public enum SpellbookType
{
    SPELLBOOK_CHAMPION,
}
public enum SpellSlotType
{
    SpellSlots,
}
public enum ForceMovementType {}
public enum ForceMovementOrdersType {}
public enum ForceMovementOrdersFacing
{
    FACE_MOVEMENT_DIRECTION,
}
public class Particle {}
public enum FXFlags {}
public enum DamageType {}
public enum DamageSource {}
public enum SpellDataFlags {}
public class Region {}
public class SpellMissile {}
public class Fade {}
public enum PrimaryAbilityResourceType {}
public enum OrderType {}
public enum ChannelingStopCondition {}
public enum ChannelingStopSource {}
public class Pet: Minion {}
public enum SpawnType {}
public enum ExtraAttributeFlag {}

public class Table {}