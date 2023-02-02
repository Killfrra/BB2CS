#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YoumusBlade : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            Particle ar; // UNUSED
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            SpellEffectCreate(out ar, out _, "spectral_fury_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            AddBuff(attacker, attacker, new Buffs.SpectralFury(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            charVars.SpectralCount = 0;
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.YoumusBlade))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name1 == nameof(Spells.YoumusBlade))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.YoumusBlade))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name3 == nameof(Spells.YoumusBlade))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name4 == nameof(Spells.YoumusBlade))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name5 == nameof(Spells.YoumusBlade))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
    }
}
namespace Buffs
{
    public class YoumusBlade : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            IncPermanentPercentCooldownMod(owner, -0.15f);
            IncPermanentFlatArmorPenetrationMod(owner, 20);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentCooldownMod(owner, 0.15f);
            IncPermanentFlatArmorPenetrationMod(owner, -20);
        }
    }
}