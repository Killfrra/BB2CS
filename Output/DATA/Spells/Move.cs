#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Move : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Move",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            NonDispellable = true,
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        float speed;
        float gravity;
        Vector3 position;
        public Move(float speed = default, float gravity = default, Vector3 position = default)
        {
            this.speed = speed;
            this.gravity = gravity;
            this.position = position;
        }
        public override void OnActivate()
        {
            Vector3 position;
            float idealDistance; // UNITIALIZED
            //RequireVar(this.gravity);
            //RequireVar(this.speed);
            //RequireVar(this.position);
            //RequireVar(this.idealDistance);
            position = this.position;
            Move(owner, position, this.speed, this.gravity, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance);
            ApplyAssistMarker(attacker, owner, 10);
            SetCanCast(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanCast(owner, true);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanCast(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
    }
}