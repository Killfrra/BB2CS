#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShenShadowDash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Shadow Dash",
            BuffTextureName = "Shen_ShadowDash.dds",
        };
        float tauntDuration;
        float energyRefunds;
        Vector3 targetPos;
        float dashSpeed;
        float distance;
        Particle selfParticle;
        public ShenShadowDash(float tauntDuration = default, float energyRefunds = default, Vector3 targetPos = default, float dashSpeed = default, float distance = default)
        {
            this.tauntDuration = tauntDuration;
            this.energyRefunds = energyRefunds;
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.distance = distance;
        }
        public override void OnCollision()
        {
            TeamId teamID; // UNUSED
            bool nextBuffVars_playParticle;
            if(owner.Team != target.Team)
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        if(GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) == 0)
                        {
                            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.ShenShadowDashCooldown)) == 0)
                            {
                                teamID = GetTeamID(owner);
                                AddBuff((ObjAIBase)owner, target, new Buffs.ShenShadowDashCooldown(), 1, 1, this.tauntDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(target);
                                nextBuffVars_playParticle = true;
                                ApplyTaunt(attacker, target, this.tauntDuration);
                                if(target is Champion)
                                {
                                    if(this.energyRefunds >= 1)
                                    {
                                        IncPAR(owner, 50, PrimaryAbilityResourceType.Energy);
                                        this.energyRefunds--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            StartTrackingCollisions(owner, true);
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 targetPos;
            teamID = GetTeamID(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.tauntDuration);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.distance);
            //RequireVar(this.energyRefunds);
            targetPos = this.targetPos;
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.selfParticle, out _, "Shen_shadowdash_mis.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            StartTrackingCollisions(owner, true);
            PlayAnimation("Dash", 0, owner, true, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            StartTrackingCollisions(owner, false);
            SpellEffectRemove(this.selfParticle);
            UnlockAnimation(owner, true);
        }
        public override void OnUpdateStats()
        {
            StartTrackingCollisions(owner, true);
        }
        public override void OnMoveEnd()
        {
            TeamId teamID;
            bool nextBuffVars_playParticle;
            Particle targetParticle; // UNUSED
            teamID = GetTeamID(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.ShenShadowDashCooldown)) == 0)
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.ShenShadowDashCooldown(), 1, 1, this.tauntDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    nextBuffVars_playParticle = true;
                    ApplyTaunt(attacker, unit, this.tauntDuration);
                    SpellEffectCreate(out targetParticle, out _, "shen_shadowDash_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, default, default, false, false);
                    if(unit is Champion)
                    {
                        if(this.energyRefunds >= 1)
                        {
                            IncPAR(owner, 40, PrimaryAbilityResourceType.Energy);
                            this.energyRefunds--;
                        }
                    }
                }
            }
            SpellBuffRemoveCurrent(owner);
        }
    }
}
namespace Spells
{
    public class ShenShadowDash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {1, 1.25f, 1.5f, 1.75f, 2};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            if(!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Vector3 ownerPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            float nextBuffVars_tauntDuration;
            float nextBuffVars_EnergyRefunds;
            float energyRefunds;
            Particle targetParticle; // UNUSED
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            moveSpeed = GetMovementSpeed(owner);
            dashSpeed = moveSpeed + 800;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            if(distance >= 575)
            {
                FaceDirection(owner, targetPos);
                distance = 575;
                targetPos = GetPointByUnitFacingOffset(owner, 575, 0);
            }
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_dashSpeed = dashSpeed;
            nextBuffVars_tauntDuration = this.effect0[level];
            energyRefunds = 1;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.ShenShadowDashCooldown)) == 0)
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.ShenShadowDashCooldown(), 1, 1, nextBuffVars_tauntDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyTaunt(attacker, unit, nextBuffVars_tauntDuration);
                    SpellEffectCreate(out targetParticle, out _, "shen_shadowDash_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                    if(unit is Champion)
                    {
                        if(energyRefunds >= 1)
                        {
                            IncPAR(owner, 40, PrimaryAbilityResourceType.Energy);
                            energyRefunds--;
                        }
                    }
                }
            }
            nextBuffVars_EnergyRefunds = energyRefunds;
            AddBuff(attacker, owner, new Buffs.ShenShadowDash(nextBuffVars_tauntDuration, nextBuffVars_EnergyRefunds, nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_Distance), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.1f, true, false, false);
        }
    }
}