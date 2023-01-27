#nullable enable

using static Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ChainMissileParameters
{
    public int? CanHitCaster;
    public int? CanHitEnemies;
    public int? CanHitFriends;
    public int? CanHitSameTarget;
    public int? CanHitSameTargetConsecutively;
    public int[]? MaximumHits;
}

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SpellScriptMetaDataNullable: IBBMetadata
{
    #region Properties
    //public string? AutoAuraBuffName;
    //public string? AutoItemActivateEffect;
    public float[]? AutoCooldownByLevel;
    public float[]? AutoTargetDamageByLevel;
    public bool? CastingBreaksStealth;
    public float? CastTime;
    public ChainMissileParameters? ChainMissileParameters;
    public float? ChannelDuration;
    public bool? DoesntBreakShields;
    public bool? TriggersSpellCasts;
    public bool? IsDamagingSpell;
    public bool? NotSingleTargetSpell;
    public float? PhysicalDamageRatio;
    public float? SpellDamageRatio;
    //public bool? IsDebugMode;    
    #endregion

    public override void Parse(Dictionary<string, object> globals, HashSet<string> used)
    {
        PhysicalDamageRatio = globals.UseValueOrDefault(used, "SpellDamageRatio") as float?;
        SpellDamageRatio =
            (globals.UseValueOrDefault(used, "SpellDamageRatio") as float?) ??
            (globals.UseValueOrDefault(used, "SetSpellDamageRatio") as float?);
             globals.UseValueOrDefault(used, "SetSpellDamageRatio"); //HACK:
        
        //AutoItemActivateEffect = globals.UseValueOrDefault(used, "AutoItemActivateEffect") as string;
        //AutoAuraBuffName = globals.UseValueOrDefault(used, "AutoAuraBuffName") as string;
        //IsDebugMode = globals.UseValueOrDefault(used, "IsDebugMode") as bool?;

        ChainMissileParameters = (globals.UseValueOrDefault(used, "ChainMissileParameters") as JObject)?.ToObject<ChainMissileParameters>();

        ChannelDuration = globals.UseValueOrDefault(used, "ChannelDuration") as float?;
        DoesntBreakShields = globals.UseValueOrDefault(used, "DoesntBreakShields") as bool?;

        TriggersSpellCasts =
            (globals.UseValueOrDefault(used, "TriggersSpellCasts") as bool?) ??
            Invert(globals.UseValueOrDefault(used, "DoesntTriggerSpellCasts") as bool?);
                   globals.UseValueOrDefault(used, "DoesntTriggerSpellCasts"); //HACK:
        
        IsDamagingSpell = globals.UseValueOrDefault(used, "IsDamagingSpell") as bool?;
        NotSingleTargetSpell = globals.UseValueOrDefault(used, "NotSingleTargetSpell") as bool?;

        float[]? ReadFloatArray(string name) =>
            (globals.UseValueOrDefault(used, name) as JArray)?.ToObject<float[]>();
        AutoCooldownByLevel = ReadFloatArray("AutoCooldownByLevel");
        AutoTargetDamageByLevel = ReadFloatArray("AutoTargetDamageByLevel");

        CastingBreaksStealth = globals.UseValueOrDefault(used, "CastingBreaksStealth") as bool?;
        CastTime = globals.UseValueOrDefault(used, "CastTime") as float?;
    }
}