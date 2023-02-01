#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiTrunkLineStun : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiStun",
            BuffTextureName = "Maokai_ArcaneSmash.dds",
        };
        public override void OnActivate()
        {
            float dist;
            dist = DistanceBetweenObjects("Attacker", "Owner");
            dist += 225;
            MoveAway(owner, attacker.Position, 200, 10, dist, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 100, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}