#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PrideCooldown : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            float aP;
            float aPBonus;
            float shieldHealth;
            float nextBuffVars_ShieldHealth;
            aP = GetFlatMagicDamageMod(owner);
            aPBonus = aP * 0.6f;
            shieldHealth = aPBonus + 200;
            nextBuffVars_ShieldHealth = shieldHealth;
            AddBuff((ObjAIBase)owner, owner, new Buffs.PrideShield(nextBuffVars_ShieldHealth), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 1);
        }
        public override void OnActivate()
        {
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(24, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name1 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(24, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(24, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name3 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(24, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name4 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(24, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name5 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(24, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
    }
}