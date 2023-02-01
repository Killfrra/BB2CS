#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class EzrealMysticShot : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            int ownerSkinID;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 1100)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1050, 0);
            }
            ownerSkinID = GetSkinID(owner);
            if(ownerSkinID == 5)
            {
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
            else
            {
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
        }
    }
}