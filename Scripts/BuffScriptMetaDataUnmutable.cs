#nullable enable

using static Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class BuffScriptMetadataUnmutable: IBBMetadata
{
    public string?[]? AutoBuffActivateAttachBoneName;
    public string?[]? AutoBuffActivateEffect;
    public string? AutoBuffActivateEvent;
    public int? AutoBuffActivateEffectFlags; //TODO: Enum { None, EFFCREATE_UPDATE_ORIENTATION }
    public string? BuffName;
    public string? BuffTextureName;
    public bool? IsDeathRecapSource;
    public bool? IsPetDurationBuff;
    public string? MinimapIconTextureName;
    public string? MinimapIconEnemyTextureName;
    public bool? NonDispellable;
    public int? OnPreDamagePriority;
    public bool? DoOnPreDamageInExpirationOrder;
    public bool? PersistsThroughDeath;
    public string?[]? PopupMessage;
    public string[]? SpellFXOverrideSkins;
    public string[]? SpellVOOverrideSkins;
    public int? SpellToggleSlot; // [1-4]

    public override void Parse(Dictionary<string, object> globals, HashSet<string> used)
    {
        BuffName = globals.UseValueOrDefault(used, "BuffName") as string;
        BuffTextureName = globals.UseValueOrDefault(used, "BuffTextureName") as string;
        
        MinimapIconTextureName = globals.UseValueOrDefault(used, "MinimapIconTextureName") as string;
        MinimapIconEnemyTextureName = globals.UseValueOrDefault(used, "MinimapIconEnemyTextureName") as string;

        AutoBuffActivateEvent = globals.UseValueOrDefault(used, "AutoBuffActivateEvent") as string;
        AutoBuffActivateEffect = globals.ReadArray<string?>(used, "AutoBuffActivateEffect", null);
        AutoBuffActivateEffectFlags = globals.UseValueOrDefault(used, "AutoBuffActivateEffectFlags") as int?;
        AutoBuffActivateAttachBoneName = globals.ReadArray<string?>(used, "AutoBuffActivateAttachBoneName", null);
        SpellToggleSlot = globals.UseValueOrDefault(used, "SpellToggleSlot") as int?;

        NonDispellable = (globals.UseValueOrDefault(used, "NonDispellable") as bool?) ?? 
                        Invert(globals.UseValueOrDefault(used, "Nondispellable") as bool?);
                               globals.UseValueOrDefault(used, "Nondispellable"); //HACK:
        
        PersistsThroughDeath = (globals.UseValueOrDefault(used, "PersistsThroughDeath") as bool?) ??
                               (globals.UseValueOrDefault(used, "PermeatesThroughDeath") as bool?);
                                globals.UseValueOrDefault(used, "PermeatesThroughDeath"); //HACK:
        
        IsPetDurationBuff = globals.UseValueOrDefault(used, "IsPetDurationBuff") as bool?;

        PopupMessage = globals.ReadArray<string?>(used, "PopupMessage", null);
        IsDeathRecapSource = globals.UseValueOrDefault(used, "IsDeathRecapSource") as bool?;
        OnPreDamagePriority = globals.UseValueOrDefault(used, "OnPreDamagePriority") as int?;
        DoOnPreDamageInExpirationOrder = globals.UseValueOrDefault(used, "DoOnPreDamageInExpirationOrder") as bool?;

        string[]? ReadStringArray(string name) =>
            (globals.UseValueOrDefault(used, name) as JArray)?.ToObject<string[]>();
        SpellFXOverrideSkins = ReadStringArray("SpellFXOverrideSkins");
        SpellVOOverrideSkins = ReadStringArray("SpellVOOverrideSkins");
    }
}