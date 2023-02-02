#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SlashCast : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                bool canMove;
                canMove = GetCanMove(owner);
                if(!canMove)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 650)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 645, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}