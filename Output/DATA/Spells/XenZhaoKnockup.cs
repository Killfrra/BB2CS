#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XenZhaoKnockup : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoKnockup",
            BuffTextureName = "XinZhao_ThreeTalon.dds",
        };
        public override void OnActivate()
        {
            Vector3 bouncePos;
            bouncePos = GetRandomPointInAreaUnit(owner, 80, 80);
            Move(owner, bouncePos, 100, 20, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 80, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}