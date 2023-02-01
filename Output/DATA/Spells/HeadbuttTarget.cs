#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HeadbuttTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Headbutt",
            BuffTextureName = "Minotaur_Headbutt.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            float distance;
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            distance = DistanceBetweenObjects("Attacker", "Owner");
            MoveAway(owner, attacker.Position, 1200, 20, 700 + distance, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
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
            SetCanMove(owner, false);
            SetCanCast(owner, false);
        }
    }
}