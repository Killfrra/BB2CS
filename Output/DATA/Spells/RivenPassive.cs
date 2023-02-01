#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RivenPassive",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = {5, 5, 5, 7, 7, 7, 9, 9, 9, 11, 11, 11, 13, 13, 13, 15, 15, 15, 15};
        int[] effect1 = {5, 5, 5, 7, 7, 7, 9, 9, 9, 11, 11, 11, 13, 13, 13, 15, 15, 15, 15};
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 5);
            SetBuffToolTipVar(3, 3);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int level; // UNUSED
            float attackDamage;
            float baseAD;
            float passiveAD;
            float bonusBaseAD;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.RivenMartyr))
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenMartyr(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(spellName == nameof(Spells.RivenFengShuiEngine))
            {
                attackDamage = GetTotalAttackDamage(owner);
                baseAD = GetBaseAttackDamage(owner);
                attackDamage -= baseAD;
                passiveAD = 0.6f * attackDamage;
                bonusBaseAD = 0.12f * baseAD;
                passiveAD = 0.6f + bonusBaseAD;
                SetBuffToolTipVar(2, passiveAD);
            }
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenPassiveAABoost(), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int count;
            int level;
            float attackDamage;
            float baseAD;
            float passiveAD;
            float baseDamage;
            float bonusDamage;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenPassiveAABoost));
            if(count > 0)
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        level = GetLevel(owner);
                        attackDamage = GetTotalAttackDamage(owner);
                        baseAD = GetBaseAttackDamage(owner);
                        attackDamage -= baseAD;
                        passiveAD = 0.5f * attackDamage;
                        baseDamage = this.effect0[level];
                        bonusDamage = baseDamage + passiveAD;
                        damageAmount += bonusDamage;
                        if(count > 1)
                        {
                            SpellBuffRemove(owner, nameof(Buffs.RivenPassiveAABoost), (ObjAIBase)owner, 5);
                        }
                        else
                        {
                            SpellBuffClear(owner, nameof(Buffs.RivenPassiveAABoost));
                        }
                    }
                }
            }
        }
        public override void OnLevelUp()
        {
            int level;
            float damageAmp;
            level = GetLevel(owner);
            damageAmp = this.effect1[level];
            SetBuffToolTipVar(1, damageAmp);
        }
        public override void OnUpdateActions()
        {
            float attackDamage;
            float baseAD;
            float passiveAD;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, false))
            {
                attackDamage = GetTotalAttackDamage(owner);
                baseAD = GetBaseAttackDamage(owner);
                attackDamage -= baseAD;
                passiveAD = 0.5f * attackDamage;
                SetBuffToolTipVar(2, passiveAD);
            }
        }
    }
}