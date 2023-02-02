#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptRiven : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                float attackDamage;
                float rAttackGain;
                float baseAD;
                float qAttackDamage;
                float rAttackDamage;
                float eAttackDamage;
                float wAttackDamage;
                attackDamage = GetTotalAttackDamage(owner);
                rAttackGain = 0.2f * attackDamage;
                SetSpellToolTipVar(rAttackGain, 3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                baseAD = GetBaseAttackDamage(owner);
                attackDamage -= baseAD;
                qAttackDamage = 0.7f * attackDamage;
                SetSpellToolTipVar(qAttackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                rAttackDamage = 0.6f * attackDamage;
                SetSpellToolTipVar(rAttackDamage, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                rAttackDamage = 1.8f * attackDamage;
                SetSpellToolTipVar(rAttackDamage, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                eAttackDamage = attackDamage * 1;
                SetSpellToolTipVar(eAttackDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                wAttackDamage = 1 * attackDamage;
                SetSpellToolTipVar(wAttackDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RivenPassive(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
        }
    }
}