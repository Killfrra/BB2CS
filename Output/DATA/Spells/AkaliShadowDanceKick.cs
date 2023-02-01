#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliShadowDanceKick : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
            IsDeathRecapSource = true,
        };
        Vector3 targetPos;
        float distance;
        float dashSpeed;
        float damageVar;
        Particle selfParticle;
        bool willRemove; // UNUSED
        public AkaliShadowDanceKick(Vector3 targetPos = default, float distance = default, float dashSpeed = default, float damageVar = default)
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
            PlayAnimation("Spell4", 0, owner, true, false, true);
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.selfParticle, out _, "akali_shadowDance_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            this.willRemove = false;
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.selfParticle);
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnMoveEnd()
        {
            SpellBuffClear(owner, nameof(Buffs.AkaliShadowDanceKick));
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            BreakSpellShields(caster);
            AddBuff((ObjAIBase)owner, caster, new Buffs.AkaliShadowDanceKickParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, this.damageVar, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 1, false, false, attacker);
            if(caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}