#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AlZaharNullZone : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.04f, 0.05f, 0.06f, 0.07f, 0.08f};
        int[] effect1 = {20, 30, 40, 50, 60};
        public override void SelfExecute()
        {
            Vector3 nextBuffVars_TargetPos;
            Vector3 targetPos;
            TeamId teamID; // UNUSED
            float healthPercent;
            float abilityPowerRatio;
            float abilityPower;
            float healthPercentPerTick;
            int nextBuffVars_HealthFlat; // UNUSED
            float nextBuffVars_HealthPercentPerTick;
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                AddBuff(attacker, attacker, new Buffs.AlZaharVoidlingCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            teamID = GetTeamID(owner);
            healthPercent = this.effect0[level];
            nextBuffVars_HealthFlat = this.effect1[level];
            abilityPowerRatio = GetFlatMagicDamageMod(owner);
            abilityPower = abilityPowerRatio * 0.0001f;
            healthPercentPerTick = healthPercent + abilityPower;
            nextBuffVars_HealthPercentPerTick = healthPercentPerTick;
            AddBuff(attacker, owner, new Buffs.AlZaharNullZone(nextBuffVars_HealthPercentPerTick, nextBuffVars_TargetPos), 5, 1, 5, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class AlZaharNullZone : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "AlZaharNullZone",
        };
        float healthPercentPerTick;
        Vector3 targetPos;
        Particle particle1;
        Particle particle2;
        float lastTimeExecuted;
        public AlZaharNullZone(float healthPercentPerTick = default, Vector3 targetPos = default)
        {
            this.healthPercentPerTick = healthPercentPerTick;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Particle varrr; // UNUSED
            //RequireVar(this.healthFlat);
            //RequireVar(this.healthPercentPerTick);
            //RequireVar(this.targetPos);
            teamOfOwner = GetTeamID(attacker);
            targetPos = this.targetPos;
            SpellEffectCreate(out varrr, out _, "AlzaharNullZoneFlash.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, owner, default, default, true, default, default, false, false);
            SpellEffectCreate(out this.particle1, out this.particle2, "AlzaharVoidPortal_flat_green.troy", "AlzaharVoidPortal_flat_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle1);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                TeamId teamOfOwner; // UNUSED
                Vector3 targetPos;
                float health;
                float damagePerTick;
                teamOfOwner = GetTeamID(attacker);
                targetPos = this.targetPos;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    health = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    damagePerTick = health * this.healthPercentPerTick;
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, default, true))
                {
                    health = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    damagePerTick = health * this.healthPercentPerTick;
                    damagePerTick = Math.Min(120, damagePerTick);
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}