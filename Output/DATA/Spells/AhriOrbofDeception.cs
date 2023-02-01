#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AhriOrbofDeception : BBSpellScript
    {
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos; // UNUSED
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 900, 0);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
    }
}