#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingNimbusKick : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        Vector3 targetPos;
        float dashSpeed;
        float attackSpeedVar;
        float damageVar;
        Particle selfParticle;
        bool willRemove; // UNUSED
        public MonkeyKingNimbusKick(Vector3 targetPos = default, float dashSpeed = default, float attackSpeedVar = default, float damageVar = default)
        {
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.attackSpeedVar = attackSpeedVar;
            this.damageVar = damageVar;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            float distance; // UNITIALIZED
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.attackSpeedVar);
            //RequireVar(this.damageVar);
            SpellEffectCreate(out this.selfParticle, out _, "monkeyKing_Q_self_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            targetPos = this.targetPos;
            PlayAnimation("Spell1", 0, owner, true, true, true);
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            this.willRemove = false;
            SetGhosted(owner, true);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetGhosted(owner, false);
            SpellEffectRemove(this.selfParticle);
        }
        public override void OnMoveEnd()
        {
            SpellBuffClear(owner, nameof(Buffs.MonkeyKingNimbusKick));
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            float nextBuffVars_AttackSpeedVar;
            caster = SetBuffCasterUnit();
            BreakSpellShields(caster);
            AddBuff((ObjAIBase)owner, caster, new Buffs.MonkeyKingNimbusKickFX(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, this.damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, (ObjAIBase)owner);
            nextBuffVars_AttackSpeedVar = this.attackSpeedVar;
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingNimbusAS(nextBuffVars_AttackSpeedVar), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if(caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}