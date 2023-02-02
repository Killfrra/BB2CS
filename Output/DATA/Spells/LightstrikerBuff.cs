#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LightstrikerBuff : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            AddBuff((ObjAIBase)owner, owner, new Buffs.LightstrikerBuff(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name1 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name3 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name4 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name5 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
    }
}
namespace Buffs
{
    public class LightstrikerBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[]{ "sword_of_the_divine_03.troy", "sword_of_the_divine_02.troy", "sword_of_the_divine_02.troy", "sword_of_the_divine_01.troy", },
            BuffName = "Lightslicer",
            BuffTextureName = "3084_Widowmaker.dds",
        };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorPenetrationMod(owner, 30);
        }
    }
}