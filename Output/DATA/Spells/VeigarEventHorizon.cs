#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VeigarEventHorizon : BBBuffScript
    {
        float stunDuration;
        Vector3 targetPos;
        Particle particle2;
        Particle particle;
        public VeigarEventHorizon(float stunDuration = default, Vector3 targetPos = default)
        {
            this.stunDuration = stunDuration;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            float nextBuffVars_StunDuration;
            Vector3 nextBuffVars_TargetPos;
            int veigarSkinID;
            //RequireVar(this.stunDuration);
            //RequireVar(this.targetPos);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            teamOfOwner = GetTeamID(owner);
            targetPos = this.targetPos;
            nextBuffVars_StunDuration = this.stunDuration;
            nextBuffVars_TargetPos = this.targetPos;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.VeigarEventHorizonMarker(nextBuffVars_StunDuration, nextBuffVars_TargetPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
            }
            veigarSkinID = GetSkinID(attacker);
            if(veigarSkinID == 4)
            {
                SpellEffectCreate(out this.particle2, out this.particle, "permission_desecrate_green_cas_leprechaun.troy", "permission_desecrate_red_cas_leprechaun.troy", teamOfOwner, 900, 0, TeamId.TEAM_BLUE, default, default, false, default, default, targetPos, default, default, this.targetPos, false, false, false, false, false);
            }
            else if(veigarSkinID == 6)
            {
                SpellEffectCreate(out this.particle2, out this.particle, "permission_desecrate_green_cas_daper.troy", "permission_desecrate_red_cas_daper.troy", teamOfOwner, 900, 0, TeamId.TEAM_BLUE, default, default, false, default, default, targetPos, default, default, this.targetPos, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle2, out this.particle, "permission_desecrate_green_cas.troy", "permission_desecrate_red_cas.troy", teamOfOwner, 900, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, this.targetPos, default, default, this.targetPos, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            float duration;
            float nextBuffVars_StunDuration;
            Vector3 nextBuffVars_TargetPos;
            targetPos = this.targetPos;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.VeigarEventHorizonMarker)) == 0)
                {
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.VeigarEventHorizonPrevent)) == 0)
                    {
                        duration = 3.05f - lifeTime;
                        nextBuffVars_StunDuration = this.stunDuration;
                        nextBuffVars_TargetPos = this.targetPos;
                        AddBuff(attacker, unit, new Buffs.VeigarEventHorizonMarker(nextBuffVars_StunDuration, nextBuffVars_TargetPos), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class VeigarEventHorizon : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 24f, 22f, 20f, 18f, 16f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {1.5f, 1.75f, 2, 2.25f, 2.5f};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_StunDuration;
            TeamId teamOfOwner;
            Minion other3;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            teamOfOwner = GetTeamID(owner);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, false, 0, false, true, (Champion)owner);
            nextBuffVars_StunDuration = this.effect0[level];
            AddBuff((ObjAIBase)owner, other3, new Buffs.VeigarEventHorizon(nextBuffVars_StunDuration, nextBuffVars_TargetPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0.1f, true, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyAssistMarker(attacker, target, 10);
        }
    }
}