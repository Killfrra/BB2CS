#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CrystallizePush : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CrystalizePush",
        };
        Vector3 targetPos;
        public CrystallizePush(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            //RequireVar(this.targetPos);
            targetPos = this.targetPos;
            Move(owner, targetPos, 600, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            ApplyAssistMarker(attacker, owner, 10);
        }
    }
}