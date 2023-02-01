﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkWOneDash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        float shieldAbsorb;
        Vector3 targetPos;
        float distance;
        float dashSpeed;
        Particle selfParticle;
        bool willRemove;
        public BlindMonkWOneDash(float shieldAbsorb = default, Vector3 targetPos = default, float distance = default, float dashSpeed = default)
        {
            this.shieldAbsorb = shieldAbsorb;
            this.targetPos = targetPos;
            this.distance = distance;
            this.dashSpeed = dashSpeed;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.shieldAbsorb);
            targetPos = this.targetPos;
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.selfParticle, out _, "blindMonk_W_self_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            PlayAnimation("Spell2", 0, owner, true, false, true);
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
            SpellBuffRemove(owner, nameof(Buffs.BlindMonkWOneDash), (ObjAIBase)owner);
            this.willRemove = true;
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            float nextBuffVars_ShieldAbsorb;
            caster = SetBuffCasterUnit();
            nextBuffVars_ShieldAbsorb = this.shieldAbsorb;
            AddBuff((ObjAIBase)owner, owner, new Buffs.BlindMonkWOneShield(nextBuffVars_ShieldAbsorb), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, caster, new Buffs.BlindMonkWOneShield(nextBuffVars_ShieldAbsorb), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}