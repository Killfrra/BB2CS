#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingNimbusKickClone : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        Vector3 targetPos;
        float dashSpeed;
        float damageVar;
        Particle selfParticle;
        bool willRemove; // UNUSED
        public MonkeyKingNimbusKickClone(Vector3 targetPos = default, float dashSpeed = default, float damageVar = default)
        {
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.damageVar = damageVar;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            float distance; // UNITIALIZED
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.damageVar);
            targetPos = this.targetPos;
            PlayAnimation("Spell1", 0, owner, true, true, true);
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.selfParticle, out _, "monkeyKing_Q_self_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            this.willRemove = false;
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.selfParticle);
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            SetNoRender(owner, true);
            SetCanMove(owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingKillClone(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID;
            Champion caster;
            teamID = GetTeamID(owner);
            caster = GetChampionBySkinName("MonkeyKing", teamID ?? TeamId.TEAM_UNKNOWN);
            ApplyDamage(caster, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, caster);
            damageAmount *= 0;
        }
        public override void OnMoveEnd()
        {
            SpellBuffClear(owner, nameof(Buffs.MonkeyKingNimbusKickClone));
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            BreakSpellShields(caster);
            AddBuff((ObjAIBase)owner, caster, new Buffs.MonkeyKingNimbusKickFX(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, this.damageVar, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
        }
    }
}