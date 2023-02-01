#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShyvanaTransformCast : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            float currentPAR;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            currentPAR = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if(currentPAR != 100)
            {
                returnValue = false;
            }
            if(!canMove)
            {
                returnValue = false;
            }
            if(!canCast)
            {
                returnValue = false;
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) > 0)
            {
                returnValue = false;
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
            targetPos = GetPointByUnitFacingOffset(owner, 75 + distance, 0);
            if(distance < 300)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 375, 0);
            }
            if(distance > 950)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}