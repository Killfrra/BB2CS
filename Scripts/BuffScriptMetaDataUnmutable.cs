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
    public EffCreate? AutoBuffActivateEffectFlags;
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
        BuffName = globals.UseValueOrDefault(used, "BuffName").As<string>();
        BuffTextureName = globals.UseValueOrDefault(used, "BuffTextureName").As<string>();
        
        MinimapIconTextureName = globals.UseValueOrDefault(used, "MinimapIconTextureName").As<string>();
        MinimapIconEnemyTextureName = globals.UseValueOrDefault(used, "MinimapIconEnemyTextureName").As<string>();

        AutoBuffActivateEvent = globals.UseValueOrDefault(used, "AutoBuffActivateEvent").As<string>();
        AutoBuffActivateEffect = globals.ReadArray<string?>(used, "AutoBuffActivateEffect", null);
        AutoBuffActivateEffectFlags = globals.UseValueOrDefault(used, "AutoBuffActivateEffectFlags").As<EffCreate?>();
        AutoBuffActivateAttachBoneName = globals.ReadArray<string?>(used, "AutoBuffActivateAttachBoneName", null);
        SpellToggleSlot = globals.UseValueOrDefault(used, "SpellToggleSlot").As<int?>();

        NonDispellable = (globals.UseValueOrDefault(used, "NonDispellable").As<bool?>()) ?? 
                        Invert(globals.UseValueOrDefault(used, "Nondispellable").As<bool?>());
                               globals.UseValueOrDefault(used, "Nondispellable"); //HACK:
        
        PersistsThroughDeath = (globals.UseValueOrDefault(used, "PersistsThroughDeath").As<bool?>()) ??
                               (globals.UseValueOrDefault(used, "PermeatesThroughDeath").As<bool?>());
                                globals.UseValueOrDefault(used, "PermeatesThroughDeath"); //HACK:
        
        IsPetDurationBuff = globals.UseValueOrDefault(used, "IsPetDurationBuff").As<bool?>();

        PopupMessage = globals.ReadArray<string?>(used, "PopupMessage", null);
        IsDeathRecapSource = globals.UseValueOrDefault(used, "IsDeathRecapSource").As<bool?>();
        OnPreDamagePriority = globals.UseValueOrDefault(used, "OnPreDamagePriority").As<int?>();
        DoOnPreDamageInExpirationOrder = globals.UseValueOrDefault(used, "DoOnPreDamageInExpirationOrder").As<bool?>();

        string[]? ReadStringArray(string name) =>
            (globals.UseValueOrDefault(used, name) as JArray)?.ToObject<string[]>();
        SpellFXOverrideSkins = ReadStringArray("SpellFXOverrideSkins");
        SpellVOOverrideSkins = ReadStringArray("SpellVOOverrideSkins");
    }
}