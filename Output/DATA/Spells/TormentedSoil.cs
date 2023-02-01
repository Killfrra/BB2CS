#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TormentedSoil : BBBuffScript
    {
        float damagePerTick;
        Vector3 targetPos;
        float mRminus;
        Particle particle2;
        Particle particle;
        float lastTimeExecuted;
        public TormentedSoil(float damagePerTick = default, Vector3 targetPos = default, float mRminus = default)
        {
            this.damagePerTick = damagePerTick;
            this.targetPos = targetPos;
            this.mRminus = mRminus;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_MRminus;
            Particle hi1; // UNUSED
            Particle hi2; // UNUSED
            //RequireVar(this.damagePerTick);
            //RequireVar(this.targetPos);
            //RequireVar(this.mRminus);
            teamOfOwner = GetTeamID(owner);
            targetPos = this.targetPos;
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_MRminus = this.mRminus;
            SpellEffectCreate(out this.particle2, out this.particle, "TormentedSoil_green_tar.troy", "TormentedSoil_red_tar.troy", teamOfOwner, 280, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                ApplyDamage(attacker, unit, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.TormentedSoilDebuff(nextBuffVars_TargetPos, nextBuffVars_MRminus), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                SpellEffectCreate(out hi1, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit, default, default, false, false, false, false, false);
                SpellEffectCreate(out hi2, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_MRminus;
            Particle hi1; // UNUSED
            Particle hi2; // UNUSED
            targetPos = this.targetPos;
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_MRminus = this.mRminus;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    ApplyDamage(attacker, unit, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.TormentedSoilDebuff(nextBuffVars_TargetPos, nextBuffVars_MRminus), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    SpellEffectCreate(out hi1, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit, default, default, false, false, false, false, false);
                    SpellEffectCreate(out hi2, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit, default, default, false, false, false, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class TormentedSoil : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {25, 40, 55, 70, 85};
        int[] effect1 = {-4, -5, -6, -7, -8};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_DamagePerTick;
            int nextBuffVars_MRminus;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_DamagePerTick = this.effect0[level];
            nextBuffVars_MRminus = this.effect1[level];
            AddBuff(attacker, attacker, new Buffs.TormentedSoil(nextBuffVars_DamagePerTick, nextBuffVars_TargetPos, nextBuffVars_MRminus), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}