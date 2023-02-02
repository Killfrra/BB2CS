#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptNidalee : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {40, 70, 100};
        int[] effect1 = {125, 175, 225};
        int[] effect2 = {150, 225, 300};
        public override void OnUpdateActions()
        {
            bool isInBrush;
            isInBrush = IsInBrush(attacker);
            if(isInBrush)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Prowl(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, true))
            {
                int ownerLevel;
                ownerLevel = GetLevel(owner);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 500, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    if(unit is Champion)
                    {
                        int unitLevel;
                        unitLevel = GetLevel(unit);
                        if(ownerLevel > unitLevel)
                        {
                            IncExp(unit, 5);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            charVars.DrippingWoundDuration = 10;
            charVars.DrippingWoundMax = 5;
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetSpellToolTipVar(40, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            SetSpellToolTipVar(125, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            SetSpellToolTipVar(150, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 3)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                charVars.TakedownDamage = this.effect0[level];
                charVars.PounceDamage = this.effect1[level];
                charVars.SwipeDamage = this.effect2[level];
                SetSpellToolTipVar(charVars.TakedownDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                SetSpellToolTipVar(charVars.PounceDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                SetSpellToolTipVar(charVars.SwipeDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}