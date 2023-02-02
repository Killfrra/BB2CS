#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CaitlynEntrapment : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void SelfExecute()
        {
            bool canMove;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            Vector3 pushbackPos;
            canMove = GetCanMove(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(distance > 800)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 850, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            pushbackPos = GetPointByUnitFacingOffset(owner, 10, 0);
            if(canMove)
            {
                AddBuff(attacker, attacker, new Buffs.CaitlynEntrapment(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                MoveAway(owner, pushbackPos, 1000, 3, 500, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            }
        }
    }
}
namespace Buffs
{
    public class CaitlynEntrapment : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "DarkBinding_tar.troy", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Spell3b", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
        }
    }
}