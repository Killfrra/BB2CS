#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserIronMan : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserIronMan",
            BuffTextureName = "Mordekaiser_IronMan.dds",
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = {35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                float maxEnergy;
                int level;
                float shieldMax;
                float shieldPercent;
                float shieldDecay;
                float baseDamage;
                float totalDamage;
                float bonusDamage;
                float tooltipNumber;
                maxEnergy = GetMaxPAR(owner, PrimaryAbilityResourceType.Shield);
                level = GetLevel(owner);
                shieldMax = level * 30;
                shieldMax += 90;
                SetBuffToolTipVar(1, shieldMax);
                shieldPercent = this.effect0[level];
                SetBuffToolTipVar(2, shieldPercent);
                shieldDecay = maxEnergy * 0.03f;
                shieldDecay *= -1;
                IncPAR(owner, shieldDecay, PrimaryAbilityResourceType.Shield);
                baseDamage = GetBaseAttackDamage(owner);
                totalDamage = GetTotalAttackDamage(owner);
                bonusDamage = totalDamage - baseDamage;
                SetSpellToolTipVar(bonusDamage, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                tooltipNumber = bonusDamage * 1.65f;
                SetSpellToolTipVar(tooltipNumber, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentEnergy;
            float dA;
            currentEnergy = GetPAR(owner, PrimaryAbilityResourceType.Shield);
            dA = damageAmount * -1;
            IncPAR(owner, dA, PrimaryAbilityResourceType.Shield);
            if(currentEnergy >= damageAmount)
            {
                damageAmount -= damageAmount;
            }
            else
            {
                damageAmount -= currentEnergy;
            }
        }
    }
}