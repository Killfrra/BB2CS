#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptGraves : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, false))
            {
                float totalAD;
                float baseAD;
                float bonusAD;
                float aD1;
                float aD3A;
                float aD3B;
                totalAD = GetTotalAttackDamage(owner);
                baseAD = GetBaseAttackDamage(owner);
                bonusAD = totalAD - baseAD;
                aD1 = bonusAD * 0.8f;
                aD3A = bonusAD * 1.4f;
                aD3B = bonusAD * 1.2f;
                SetSpellToolTipVar(aD1, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                SetSpellToolTipVar(aD3A, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                SetSpellToolTipVar(aD3B, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.ArmorAmount = this.effect0[level];
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level >= 0)
                    {
                        float cD;
                        cD = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(cD > 0)
                        {
                            cD--;
                            SetSlotSpellCooldownTimeVer2(cD, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                        }
                    }
                }
            }
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.GravesPassiveCounter(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRenew(owner, nameof(Buffs.GravesPassiveGrit), 4);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.GravesPassiveCounter(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRenew(owner, nameof(Buffs.GravesPassiveGrit), 4);
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.GravesPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.PassiveDuration = 3;
            charVars.PassiveMaxStacks = 10;
            charVars.ArmorAmount = 1;
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}