#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HowlingGaleMove : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        Vector3 bouncePos;
        public HowlingGaleMove(Vector3 bouncePos = default)
        {
            this.bouncePos = bouncePos;
        }
        public override void OnActivate()
        {
            Vector3 bouncePos;
            //RequireVar(this.bouncePos);
            bouncePos = this.bouncePos;
            Move(owner, bouncePos, 100, 20, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 100);
        }
    }
}