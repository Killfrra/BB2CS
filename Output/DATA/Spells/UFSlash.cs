#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UFSlash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            SpellFXOverrideSkins = new[]{ "ReefMalphite", },
        };
        Vector3 targetPos;
        float damage;
        float stunDuration;
        float slashSpeed;
        Particle selfParticle;
        public UFSlash(Vector3 targetPos = default, float damage = default, float stunDuration = default, float slashSpeed = default)
        {
            this.targetPos = targetPos;
            this.damage = damage;
            this.stunDuration = stunDuration;
            this.slashSpeed = slashSpeed;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 targetPos;
            teamID = GetTeamID(owner);
            //RequireVar(this.willMove);
            //RequireVar(this.targetPos);
            //RequireVar(this.damage);
            //RequireVar(this.stunDuration);
            targetPos = this.targetPos;
            Move(owner, targetPos, this.slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0);
            PlayAnimation("Spell4", 0, owner, true, false);
            SpellEffectCreate(out this.selfParticle, out _, "UnstoppableForce_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, false);
            SpellEffectRemove(this.selfParticle);
            SpellBuffRemove(owner, nameof(Buffs.UnstoppableForceMarker), (ObjAIBase)owner);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.UnstoppableForceMarker), (ObjAIBase)owner);
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            TeamId teamID;
            float nextBuffVars_StunDuration;
            Particle targetParticle; // UNUSED
            teamID = GetTeamID(owner);
            nextBuffVars_StunDuration = this.stunDuration;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out targetParticle, out _, "UnstoppableForce_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, this.damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                AddBuff((ObjAIBase)owner, unit, new Buffs.UnstoppableForceStun(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false);
            }
        }
    }
}
namespace Spells
{
    public class UFSlash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {200, 300, 400, 270, 330};
        float[] effect1 = {1.5f, 1.75f, 2};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            canMove = GetCanMove(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Damage;
            float nextBuffVars_SlashSpeed;
            float nextBuffVars_StunDuration;
            Vector3 ownerPos;
            float moveSpeed;
            float slashSpeed;
            float distance;
            float duration;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Damage = this.effect0[level];
            ownerPos = GetUnitPosition(owner);
            moveSpeed = GetMovementSpeed(owner);
            slashSpeed = moveSpeed + 1000;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            duration = distance / slashSpeed;
            nextBuffVars_SlashSpeed = slashSpeed;
            nextBuffVars_StunDuration = this.effect1[level];
            AddBuff(attacker, owner, new Buffs.UFSlash(nextBuffVars_TargetPos, nextBuffVars_Damage, nextBuffVars_StunDuration, nextBuffVars_SlashSpeed), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnstoppableForceMarker(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}