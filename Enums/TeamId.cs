public enum TeamId : uint
{
    TEAM_UNKNOWN = 0,
    TEAM_CASTER = 0, //TODO:
    TEAM_BLUE = 1 << 0,
    TEAM_PURPLE = 1 << 1,
    TEAM_NEUTRAL = 0x80000000,
    TEAM_ALL = 0xFFFFFFFF,
}