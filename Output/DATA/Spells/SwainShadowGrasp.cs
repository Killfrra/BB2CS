#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SwainShadowGrasp : BBBuffScript
    {
        float graspDamage;
        float rootDuration;
        Particle groundParticleEffect;
        Particle groundParticleEffect2;
        Particle a;
        public SwainShadowGrasp(float graspDamage = default, float rootDuration = default)
        {
            this.graspDamage = graspDamage;
            this.rootDuration = rootDuration;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.graspDamage);
            //RequireVar(this.rootDuration);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.groundParticleEffect, out this.groundParticleEffect2, "Swain_shadowGrasp_warning_green.troy", "Swain_shadowGrasp_warning_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.a, out _, "swain_shadowGrasp_transform.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.groundParticleEffect);
            SpellEffectRemove(this.groundParticleEffect2);
            SpellEffectRemove(this.a);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.graspDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.SwainShadowGraspRoot(), 1, 1, this.rootDuration, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, true, false);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}
namespace Spells
{
    public class SwainShadowGrasp : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = {2, 2, 2, 2, 2};
        int[] effect1 = {80, 120, 160, 200, 240};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            Minion other3;
            float nextBuffVars_RootDuration;
            float nextBuffVars_GraspDamage;
            targetPos = GetCastSpellTargetPos();
            teamOfOwner = GetTeamID(owner);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            nextBuffVars_RootDuration = this.effect0[level];
            nextBuffVars_GraspDamage = this.effect1[level];
            AddBuff(attacker, other3, new Buffs.SwainShadowGrasp(nextBuffVars_GraspDamage, nextBuffVars_RootDuration), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}