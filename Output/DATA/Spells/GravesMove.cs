#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GravesMove : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID; // UNUSED
            Vector3 targetPos;
            Vector3 ownerPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            StopMove(attacker);
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            moveSpeed = GetMovementSpeed(owner);
            dashSpeed = moveSpeed + 850;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            if(distance > 425)
            {
                distance = 425;
            }
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_dashSpeed = dashSpeed;
            AddBuff(attacker, owner, new Buffs.GravesMove(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_Distance), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.1f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GravesMove : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Shadow Dash",
            BuffTextureName = "Shen_ShadowDash.dds",
        };
        Vector3 targetPos;
        float dashSpeed;
        float distance;
        Particle selfParticle;
        float[] effect0 = {0.4f, 0.5f, 0.6f, 0.7f, 0.8f};
        public GravesMove(Vector3 targetPos = default, float dashSpeed = default, float distance = default)
        {
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 targetPos;
            int level;
            float nextBuffVars_AttackSpeedMod;
            teamID = GetTeamID(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.distance);
            targetPos = this.targetPos;
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.selfParticle, out _, "Graves_Move_OnBuffActivate.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            PlayAnimation("Spell3", 0, owner, true, false, true);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_AttackSpeedMod = this.effect0[level];
            AddBuff(attacker, attacker, new Buffs.GravesMoveSteroid(nextBuffVars_AttackSpeedMod), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level; // UNUSED
            bool cast; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cast = false;
            StartTrackingCollisions(owner, false);
            SpellEffectRemove(this.selfParticle);
            UnlockAnimation(owner, true);
            StopMove(owner);
        }
        public override void OnUpdateStats()
        {
            StartTrackingCollisions(owner, true);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}