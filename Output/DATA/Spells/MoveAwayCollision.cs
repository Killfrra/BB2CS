#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MoveAwayCollision : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MoveAway",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        float speed;
        float gravity;
        Vector3 center;
        float distance;
        public MoveAwayCollision(float speed = default, float gravity = default, Vector3 center = default, float distance = default)
        {
            this.speed = speed;
            this.gravity = gravity;
            this.center = center;
            this.distance = distance;
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
            MoveAway(owner, center, this.speed, this.gravity, this.distance, 0, ForceMovementType.FIRST_COLLISION_HIT, ForceMovementOrdersType.CANCEL_ORDER, idealDistance);
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