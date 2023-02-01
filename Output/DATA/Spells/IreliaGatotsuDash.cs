#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IreliaGatotsuDash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "IreliaGatotsuDash",
            BuffTextureName = "Irelia_Bladesurge.dds",
            IsDeathRecapSource = true,
        };
        Vector3 targetPos;
        float distance;
        float dashSpeed;
        float damageVar;
        Particle selfParticle;
        bool willRemove;
        public IreliaGatotsuDash(Vector3 targetPos = default, float distance = default, float dashSpeed = default, float damageVar = default)
        {
            this.targetPos = targetPos;
            this.distance = distance;
            this.dashSpeed = dashSpeed;
            this.damageVar = damageVar;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.damageVar);
            targetPos = this.targetPos;
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.selfParticle, out _, "irelia_gotasu_dash_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
            PlayAnimation("spell1", 0.5f, owner, false, true, true);
            this.willRemove = false;
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.selfParticle);
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.IreliaGatotsuDash), (ObjAIBase)owner, 0);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(!caster.IsDead)
            {
                AddBuff(caster, owner, new Buffs.IreliaGatotsu(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            caster = SetBuffCasterUnit();
            BreakSpellShields(caster);
            AddBuff((ObjAIBase)owner, caster, new Buffs.IreliaGatotsuDashParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, this.damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            this.willRemove = true;
            if(caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}