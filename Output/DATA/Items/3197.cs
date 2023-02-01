﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3197 : BBItemScript
    {
        float lastTimeExecuted;
        float lastTimeExecuted2;
        public override void OnUpdateStats()
        {
            float health;
            float healthIncAmount; // UNUSED
            health = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            healthIncAmount = health * 0.15f;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(9, ref this.lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.ViktorHexCore(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpellToolTipVar(charVars.BonusForItem, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            if(ExecutePeriodically(3, ref this.lastTimeExecuted2, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.ViktorAugmentW(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}