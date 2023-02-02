#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RivenTriCleave : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {13, 13, 13, 13, 13};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            int count;
            int nextBuffVars_Count;
            float distance;
            float maxRange;
            Vector3 pos;
            Vector3 castPos;
            bool lockOn;
            bool canSee;
            AttackableUnit other1;
            Vector3 checkPos;
            bool canMove;
            bool pathable;
            Vector3 nextBuffVars_TargetPos; // UNUSED
            SpellBuffClear(owner, nameof(Buffs.RivenTriCleaveUnlock));
            CancelAutoAttack(owner, true);
            targetPos = GetCastSpellTargetPos();
            count = GetBuffCountFromCaster(owner, default, nameof(Buffs.RivenTriCleave));
            AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleave(), 3, 1, 3.75f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if(count == 0)
            {
                float cDReduction;
                float baseCD;
                float lowerCD;
                float newCD;
                cDReduction = GetPercentCooldownMod(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                baseCD = this.effect0[level];
                lowerCD = baseCD * cDReduction;
                newCD = baseCD + lowerCD;
                newCD *= 1;
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveCooldown(), 1, 1, newCD, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            distance = 240;
            maxRange = 240;
            if(distance >= maxRange)
            {
                pos = GetPointByUnitFacingOffset(owner, maxRange, 0);
            }
            else
            {
                distance = Math.Max(distance, 75);
                pos = GetPointByUnitFacingOffset(owner, distance, 0);
            }
            castPos = GetCastSpellTargetPos();
            lockOn = false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, castPos, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
            {
                canSee = CanSeeTarget(owner, unit);
                if(canSee)
                {
                    other1 = SetUnit(unit);
                    lockOn = true;
                    IssueOrder(owner, OrderType.AttackTo, default, unit);
                }
            }
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, castPos, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                canSee = CanSeeTarget(owner, unit);
                if(canSee)
                {
                    other1 = SetUnit(unit);
                    lockOn = true;
                    IssueOrder(owner, OrderType.AttackTo, default, unit);
                }
            }
            if(lockOn)
            {
                bool isMoving;
                FaceDirection(owner, other1.Position);
                distance = DistanceBetweenObjects("Owner", "Other1");
                isMoving = IsMoving(other1);
                if(!isMoving)
                {
                    distance -= 25;
                }
                else
                {
                    Vector3 leadPos;
                    leadPos = GetPointByUnitFacingOffset(other1, 125, 0);
                    FaceDirection(owner, leadPos);
                    distance = DistanceBetweenObjectAndPoint(owner, leadPos);
                }
                if(distance >= maxRange)
                {
                    pos = GetPointByUnitFacingOffset(owner, maxRange, 0);
                }
                else
                {
                    distance = Math.Max(distance, 25);
                    pos = GetPointByUnitFacingOffset(owner, distance, 0);
                }
            }
            nextBuffVars_Count = count;
            nextBuffVars_TargetPos = targetPos;
            checkPos = GetPointByUnitFacingOffset(owner, 75, 0);
            canMove = GetCanMove(owner);
            if(!canMove)
            {
                pos = GetPointByUnitFacingOffset(owner, 40, 0);
            }
            pathable = IsPathable(checkPos);
            if(!pathable)
            {
                checkPos = GetPointByUnitFacingOffset(owner, 125, 0);
                pathable = IsPathable(checkPos);
                if(!pathable)
                {
                    pos = GetPointByUnitFacingOffset(owner, 75, 180);
                }
            }
            if(count == 0)
            {
                UnlockAnimation(owner, true);
                PlayAnimation("Spell1a", 0, owner, false, true, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveSoundOne(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveBuffer(), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SetSlotSpellCooldownTimeVer2(0.25f, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveDamage(nextBuffVars_Count), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                Move(owner, pos, 825, 15, 15, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 275, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            }
            if(count == 1)
            {
                UnlockAnimation(owner, true);
                PlayAnimation("Spell1b", 0, owner, false, true, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveSoundTwo(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveBufferB(), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SetSlotSpellCooldownTimeVer2(0.25f, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveDamage(nextBuffVars_Count), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                Move(owner, pos, 825, 15, 15, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 275, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            }
            if(count == 2)
            {
                UnlockAnimation(owner, true);
                PlayAnimation("Spell1c", 0, owner, false, true, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveSoundThree(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveDamage(nextBuffVars_Count), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                Move(owner, pos, 900, 75, 15, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 375, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
                SpellBuffClear(owner, nameof(Buffs.RivenTriCleave));
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.RivenSword(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RivenTriCleave : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RivenTriCleaveBuff",
            BuffTextureName = "RivenBrokenWings.dds",
            SpellToggleSlot = 1,
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenTriCleave));
            SetBuffToolTipVar(1, count);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.RivenTriCleaveCooldown));
        }
    }
}