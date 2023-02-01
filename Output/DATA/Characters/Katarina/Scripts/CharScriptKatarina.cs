#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptKatarina : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float bbBonusDamage;
            float dlBonusDamage;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                totalDamage = GetTotalAttackDamage(owner);
                baseDamage = GetBaseAttackDamage(owner);
                bonusDamage = totalDamage - baseDamage;
                bbBonusDamage = bonusDamage * 0.8f;
                SetSpellToolTipVar(bbBonusDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                dlBonusDamage = bonusDamage * 0.5f;
                SetSpellToolTipVar(dlBonusDamage, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.Voracity(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.KillerInstinctBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}