#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XenZhaoSweep : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoSweep",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float damageDealt;
        Vector3 targetPos;
        float distance;
        Particle a;
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public XenZhaoSweep(float damageDealt = default, Vector3 targetPos = default, float distance = default)
        {
            this.damageDealt = damageDealt;
            this.targetPos = targetPos;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            OverrideAnimation("Run", "Spell1", owner);
            //RequireVar(this.damageDealt);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.bonusDamage);
            targetPos = this.targetPos;
            SpellEffectCreate(out this.a, out _, "xenZiou_AudaciousCharge_self_trail_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SetCanMove(owner, false);
            Move(target, targetPos, 3000, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanMove(owner, true);
            ClearOverrideAnimation("Run", owner);
            SpellEffectRemove(this.a);
        }
        public override void OnMoveEnd()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            SpellBuffRemove(owner, nameof(Buffs.XenZhaoSweep), caster, 0);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            int level;
            float nextBuffVars_MoveSpeedMod;
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, caster.Position, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, this.damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, (ObjAIBase)owner);
                AddBuff((ObjAIBase)owner, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            if(caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}
namespace Spells
{
    public class XenZhaoSweep : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle targetParticle; // UNUSED
        int[] effect0 = {70, 110, 150, 190, 230};
        int[] effect1 = {80, 120, 160, 200, 240};
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
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float nextBuffVars_DamageDealt;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            int nextBuffVars_BonusDamage;
            SpellEffectCreate(out this.targetParticle, out _, "xenZiou_AudaciousCharge_tar_unit_instant.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            targetPos = GetUnitPosition(target);
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            nextBuffVars_DamageDealt = this.effect0[level];
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_BonusDamage = this.effect1[level];
            AddBuff((ObjAIBase)target, attacker, new Buffs.XenZhaoSweep(nextBuffVars_DamageDealt, nextBuffVars_TargetPos, nextBuffVars_Distance), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}