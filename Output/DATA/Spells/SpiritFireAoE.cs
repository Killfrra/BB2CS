#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SpiritFireAoE : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float initialDamage;
        float damage;
        float armorReduction;
        Vector3 targetPos;
        Particle c;
        Particle boom2;
        Particle boom;
        float count;
        float lastTimeExecuted;
        public SpiritFireAoE(float initialDamage = default, float damage = default, float armorReduction = default, Vector3 targetPos = default)
        {
            this.initialDamage = initialDamage;
            this.damage = damage;
            this.armorReduction = armorReduction;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_ArmorReduction;
            //RequireVar(this.initialDamage);
            //RequireVar(this.damage);
            //RequireVar(this.armorReduction);
            //RequireVar(this.targetPos);
            targetPos = this.targetPos;
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.c, out _, "nassus_spiritFire_afterburn.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.boom2, out this.boom, "nassus_spiritFire_tar_green.troy", "nassus_spiritFire_tar_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_ArmorReduction = this.armorReduction;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                ApplyDamage(attacker, unit, this.initialDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.SpiritFireArmorReduction(nextBuffVars_TargetPos, nextBuffVars_ArmorReduction), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
            }
            this.count = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.boom);
            SpellEffectRemove(this.boom2);
            SpellEffectRemove(this.c);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            targetPos = this.targetPos;
            if(this.count < 5)
            {
                if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
                {
                    Vector3 nextBuffVars_TargetPos;
                    float nextBuffVars_ArmorReduction;
                    this.count++;
                    nextBuffVars_TargetPos = targetPos;
                    nextBuffVars_ArmorReduction = this.armorReduction;
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                    {
                        float totalDamage;
                        totalDamage = this.damage / 5;
                        ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.12f, 0, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.SpiritFireArmorReduction(nextBuffVars_TargetPos, nextBuffVars_ArmorReduction), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
                    }
                }
            }
        }
    }
}