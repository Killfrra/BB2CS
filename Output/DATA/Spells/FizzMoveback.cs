#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzMoveback : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        Vector3 centerPos;
        public FizzMoveback(Vector3 centerPos = default)
        {
            this.centerPos = centerPos;
        }
        public override void OnActivate()
        {
            float distance;
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            //RequireVar(this.centerPos);
            distance = DistanceBetweenObjectAndPoint(owner, this.centerPos);
            distance += 150;
            MoveAway(owner, this.centerPos, 750, 35, distance, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
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