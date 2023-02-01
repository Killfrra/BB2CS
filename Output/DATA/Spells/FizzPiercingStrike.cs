#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzPiercingStrike : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoSweep",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float damageDealt;
        Particle a;
        bool hitTarget;
        Particle targetParticle; // UNUSED
        public FizzPiercingStrike(float damageDealt = default)
        {
            this.damageDealt = damageDealt;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            float distance;
            float totalAD;
            //RequireVar(this.damageDealt);
            //RequireVar(this.ownerPos);
            //RequireVar(this.bonusDamage);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.a, out _, "Fizz_PiercingStrike.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, true, false, false, false, false);
            this.hitTarget = false;
            IncAcquisitionRangeMod(owner, -175);
            SetCanAttack(owner, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            attacker = SetBuffCasterUnit();
            if(!this.hitTarget)
            {
                distance = DistanceBetweenObjects("Attacker", "Owner");
                if(distance <= 175)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, this.damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
                    totalAD = GetTotalAttackDamage(owner);
                    SetDodgePiercing(owner, true);
                    ApplyDamage((ObjAIBase)owner, attacker, totalAD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, (ObjAIBase)owner);
                    this.hitTarget = true;
                    teamID = GetTeamID(owner);
                    SpellEffectCreate(out this.targetParticle, out _, "Fizz_PiercingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                    SetDodgePiercing(owner, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SpellEffectRemove(this.a);
            UnlockAnimation(owner, true);
            IncAcquisitionRangeMod(owner, 0);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            float distance;
            float totalAD;
            TeamId teamID;
            attacker = SetBuffCasterUnit();
            if(!this.hitTarget)
            {
                distance = DistanceBetweenObjects("Attacker", "Owner");
                if(distance <= 175)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, this.damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
                    totalAD = GetTotalAttackDamage(owner);
                    SetDodgePiercing(owner, true);
                    ApplyDamage((ObjAIBase)owner, attacker, totalAD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, (ObjAIBase)owner);
                    this.hitTarget = true;
                    teamID = GetTeamID(owner);
                    SpellEffectCreate(out this.targetParticle, out _, "Fizz_PiercingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                    SetDodgePiercing(owner, false);
                }
            }
            IncAcquisitionRangeMod(owner, -175);
            SetCanAttack(owner, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnMoveEnd()
        {
            ObjAIBase caster; // UNUSED
            caster = SetBuffCasterUnit();
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            UnlockAnimation(owner, true);
            SpellBuffClear(owner, nameof(Buffs.FizzPiercingStrike));
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            Vector3 targetPos; // UNUSED
            float totalAD;
            TeamId teamID;
            caster = SetBuffCasterUnit();
            targetPos = GetPointByUnitFacingOffset(owner, 275, 0);
            SpellBuffClear(owner, nameof(Buffs.FizzPiercingStrike));
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            if(!this.hitTarget)
            {
                BreakSpellShields(caster);
                ApplyDamage((ObjAIBase)owner, caster, this.damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
                totalAD = GetTotalAttackDamage(owner);
                SetDodgePiercing(owner, true);
                ApplyDamage((ObjAIBase)owner, caster, totalAD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, (ObjAIBase)owner);
                teamID = GetTeamID(owner);
                SpellEffectCreate(out this.targetParticle, out _, "Fizz_PiercingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                SetDodgePiercing(owner, false);
            }
            CancelAutoAttack(owner, false);
            UnlockAnimation(owner, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnMoveFailure()
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            UnlockAnimation(owner, true);
            SpellBuffClear(owner, nameof(Buffs.FizzPiercingStrike));
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}
namespace Spells
{
    public class FizzPiercingStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {10, 40, 70, 100, 130};
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
            float okayCheckDistance;
            float checkDistance;
            float leapDistance;
            float doubleCheckDistance;
            bool result;
            float nextBuffVars_DamageDealt;
            Vector3 nextBuffVars_OwnerPos;
            targetPos = GetUnitPosition(target);
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            okayCheckDistance = 0;
            checkDistance = 0;
            leapDistance = 600 - distance;
            FaceDirection(owner, target.Position);
            while(checkDistance <= leapDistance)
            {
                doubleCheckDistance = checkDistance + distance;
                targetPos = GetPointByUnitFacingOffset(owner, doubleCheckDistance, 0);
                result = IsPathable(targetPos);
                if(!result)
                {
                    checkDistance += 601;
                }
                else
                {
                    okayCheckDistance = checkDistance;
                }
                checkDistance += 25;
            }
            distance += okayCheckDistance;
            targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            nextBuffVars_DamageDealt = this.effect0[level];
            nextBuffVars_OwnerPos = ownerPos;
            Move(owner, targetPos, 1450, 0, 25, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            PlayAnimation("Spell1", 0, owner, false, false, false);
            AddBuff((ObjAIBase)target, attacker, new Buffs.FizzPiercingStrike(nextBuffVars_DamageDealt), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(target is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, target);
            }
        }
    }
}