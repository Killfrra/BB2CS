#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MoveAway : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MoveAway",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            NonDispellable = true,
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        float distance;
        float gravity;
        float speed;
        Vector3 center;
        public MoveAway(float distance = default, float gravity = default, float speed = default, Vector3 center = default)
        {
            this.distance = distance;
            this.gravity = gravity;
            this.speed = speed;
            this.center = center;
        }
        public override void OnActivate()
        {
            Vector3 center;
            float idealDistance; // UNITIALIZED
            //RequireVar(this.distance);
            //RequireVar(this.idealDistance);
            //RequireVar(this.gravity);
            //RequireVar(this.speed);
            //RequireVar(this.center);
            center = this.center;
            MoveAway(owner, center, this.speed, this.gravity, this.distance, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance);
            ApplyAssistMarker(attacker, owner, 10);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateStats()
        {
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
    }
}