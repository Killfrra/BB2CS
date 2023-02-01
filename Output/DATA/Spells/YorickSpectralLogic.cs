#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickSpectralLogic : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "YorickSpectralGhoul",
            BuffTextureName = "YorickOmenOfWar.dds",
        };
        int startingLevel;
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(type == BuffType.SLOW)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            float aDFromLevel;
            float tAD;
            float maxHealth;
            float aDFromStats;
            float healthFromStats;
            this.startingLevel = GetLevel(attacker);
            aDFromLevel = 2 * this.startingLevel;
            IncPermanentFlatArmorMod(owner, aDFromLevel);
            IncPermanentFlatSpellBlockMod(owner, aDFromLevel);
            tAD = GetTotalAttackDamage(attacker);
            maxHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.MANA);
            aDFromStats = tAD * 0.35f;
            aDFromStats -= 10;
            aDFromStats = Math.Max(aDFromStats, 10);
            healthFromStats = maxHealth * 0.35f;
            healthFromStats -= 60;
            healthFromStats = Math.Max(healthFromStats, 60);
            IncPermanentFlatHPPoolMod(owner, healthFromStats);
            IncPermanentFlatPhysicalDamageMod(owner, aDFromStats);
            if(this.startingLevel >= 3)
            {
                IncPermanentFlatMovementSpeedMod(owner, 30);
            }
            if(this.startingLevel >= 6)
            {
                IncPermanentFlatMovementSpeedMod(owner, 30);
            }
            if(this.startingLevel >= 11)
            {
                IncPermanentFlatMovementSpeedMod(owner, 40);
            }
            IncPermanentPercentHPRegenMod(owner, -1);
        }
        public override void OnUpdateActions()
        {
            float maxHealth;
            if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
            {
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                maxHealth *= 0.2f;
                ApplyDamage((ObjAIBase)owner, owner, maxHealth, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            }
        }
        public override void OnPreMitigationDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource == default)
            {
                damageAmount *= 0.5f;
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            ApplyDamage(caster, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, caster);
            damageAmount *= 0;
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float effectiveHeal;
            if(health >= 0)
            {
                effectiveHeal = health * 0;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}