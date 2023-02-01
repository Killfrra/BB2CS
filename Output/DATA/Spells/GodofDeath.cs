#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GodofDeath : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[]{ "nassus_godofDeath_sword.troy", "nassus_godofDeath_overhead.troy", "nassus_godofDeath_overhead.troy", "", },
            BuffName = "GodofDeath",
            BuffTextureName = "Nasus_AvatarOfDeath.dds",
        };
        Particle auraParticle;
        float damageCap;
        float damagePerc;
        float currentDamageTotal;
        float bonusHealth;
        float lastTimeExecuted;
        public GodofDeath(float damageCap = default, float damagePerc = default, float currentDamageTotal = default, float bonusHealth = default)
        {
            this.damageCap = damageCap;
            this.damagePerc = damagePerc;
            this.currentDamageTotal = currentDamageTotal;
            this.bonusHealth = bonusHealth;
        }
        public override void OnActivate()
        {
            float damageCap; // UNUSED
            float damagePerc;
            float temp1;
            float abilityPowerMod;
            float abilityPowerBonus;
            float hToDamage;
            SpellEffectCreate(out this.auraParticle, out _, "nassus_godofDeath_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.damageCap);
            //RequireVar(this.damagePerc);
            //RequireVar(this.currentDamageTotal);
            //RequireVar(this.bonusHealth);
            damageCap = this.damageCap;
            damagePerc = this.damagePerc;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                abilityPowerMod = GetFlatMagicDamageMod(owner);
                abilityPowerBonus = abilityPowerMod * 0.0001f;
                damagePerc += abilityPowerBonus;
                hToDamage = damagePerc * temp1;
                hToDamage = Math.Min(hToDamage, 240);
                ApplyDamage(attacker, unit, hToDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                hToDamage *= 0.05f;
                this.currentDamageTotal += hToDamage;
            }
            SetBuffToolTipVar(1, this.currentDamageTotal);
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out _, out _, "nassus_godofDeath_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectRemove(this.auraParticle);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, this.bonusHealth);
            IncFlatPhysicalDamageMod(owner, this.currentDamageTotal);
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnUpdateActions()
        {
            float damageCap;
            float damagePerc;
            float temp1;
            float abilityPowerMod;
            float abilityPowerBonus;
            float hToDamage;
            damageCap = this.damageCap;
            damagePerc = this.damagePerc;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    abilityPowerMod = GetFlatMagicDamageMod(owner);
                    abilityPowerBonus = abilityPowerMod * 0.0001f;
                    damagePerc += abilityPowerBonus;
                    hToDamage = damagePerc * temp1;
                    hToDamage = Math.Min(hToDamage, 240);
                    ApplyDamage(attacker, unit, hToDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    hToDamage *= 0.05f;
                    this.currentDamageTotal += hToDamage;
                }
                if(this.currentDamageTotal >= damageCap)
                {
                    this.currentDamageTotal = Math.Min(this.currentDamageTotal, damageCap);
                }
                SetBuffToolTipVar(1, this.currentDamageTotal);
            }
        }
    }
}
namespace Spells
{
    public class GodofDeath : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {300, 300, 300, 0, 0};
        float[] effect1 = {0.03f, 0.04f, 0.05f, 0, 0};
        int[] effect2 = {300, 450, 600, 0, 0};
        int[] effect3 = {15, 15, 15};
        public override void SelfExecute()
        {
            int nextBuffVars_DamageCap;
            float nextBuffVars_DamagePerc;
            float nextBuffVars_CurrentDamageTotal;
            float nextBuffVars_BonusHealth;
            nextBuffVars_DamageCap = this.effect0[level];
            nextBuffVars_DamagePerc = this.effect1[level];
            nextBuffVars_CurrentDamageTotal = 0;
            nextBuffVars_BonusHealth = this.effect2[level];
            AddBuff(attacker, owner, new Buffs.GodofDeath(nextBuffVars_DamageCap, nextBuffVars_DamagePerc, nextBuffVars_CurrentDamageTotal, nextBuffVars_BonusHealth), 1, 1, this.effect3[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}