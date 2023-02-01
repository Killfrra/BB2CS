#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleWallPush : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleWallPush",
            BuffTextureName = "Trundle_Pillar.dds",
        };
        float trueMove;
        public TrundleWallPush(float trueMove = default)
        {
            this.trueMove = trueMove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.trueMove);
            MoveAway(owner, attacker.Position, 750, 0, this.trueMove, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.trueMove);
        }
    }
}