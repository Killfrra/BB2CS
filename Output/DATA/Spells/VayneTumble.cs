#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneTumble : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hnd", "r_hnd", },
            AutoBuffActivateEffect = new[]{ "Global_Haste.troy", "Global_Haste.troy", },
            BuffName = "VayneTumble",
            BuffTextureName = "Renekton_SliceAndDice.dds",
        };
        float dashSpeed;
        Vector3 targetPos;
        float distance;
        bool failed; // UNUSED
        Particle shinyParticle; // UNUSED
        public VayneTumble(float dashSpeed = default, Vector3 targetPos = default, float distance = default)
        {
            this.dashSpeed = dashSpeed;
            this.targetPos = targetPos;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            Vector3 targetPos;
            Particle hi; // UNUSED
            teamID = GetTeamID(owner);
            PlayAnimation("Spell1", 0, owner, false, false, true);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            this.failed = false;
            targetPos = this.targetPos;
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            Move(owner, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
            {
                SpellEffectCreate(out hi, out _, "vayne_ult_invis_cas.troy", default, TeamId.TEAM_NEUTRAL, 150, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position, owner, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.shinyParticle, out _, "vayne_q_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, target, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            owner = SetBuffCasterUnit();
            UnlockAnimation(owner, true);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.VayneTumble), (ObjAIBase)owner, 0);
        }
    }
}
namespace Spells
{
    public class VayneTumble : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
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
            TeamId teamID; // UNUSED
            Vector3 targetPos;
            Vector3 ownerPos;
            float moveSpeed;
            float dashSpeed; // UNUSED
            float distance;
            Particle hi; // UNUSED
            float nextBuffVars_DashSpeed;
            float nextBuffVars_Distance;
            Vector3 nextBuffVars_TargetPos;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            moveSpeed = GetMovementSpeed(owner);
            dashSpeed = moveSpeed + 500;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            SpellEffectCreate(out hi, out _, "vayne_ult_invis_cas_02.troy", default, TeamId.TEAM_NEUTRAL, 150, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position, owner, default, default, true, false, false, false, false);
            if(distance >= 0)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 300, 0);
            }
            nextBuffVars_DashSpeed = 900;
            nextBuffVars_Distance = 300;
            nextBuffVars_TargetPos = targetPos;
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff(attacker, owner, new Buffs.VayneTumble(nextBuffVars_DashSpeed, nextBuffVars_TargetPos, nextBuffVars_Distance), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.1f, true, false, false);
            CancelAutoAttack(owner, true);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.VayneTumbleFade(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            AddBuff(attacker, owner, new Buffs.VayneTumbleBonus(), 1, 1, 6.75f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0.1f, true, false, false);
        }
    }
}