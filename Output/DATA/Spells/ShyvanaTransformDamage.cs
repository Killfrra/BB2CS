#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaTransformDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Move",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            NonDispellable = true,
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        float gravity;
        float speed;
        Vector3 position;
        public ShyvanaTransformDamage(float gravity = default, float speed = default, Vector3 position = default)
        {
            this.gravity = gravity;
            this.speed = speed;
            this.position = position;
        }
        public override void OnActivate()
        {
            Vector3 position;
            float idealDistance; // UNITIALIZED
            TeamId teamID;
            Particle targetParticle; // UNUSED
            //RequireVar(this.gravity);
            //RequireVar(this.speed);
            //RequireVar(this.position);
            //RequireVar(this.idealDistance);
            position = this.position;
            Move(owner, position, this.speed, this.gravity, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            ApplyAssistMarker(attacker, owner, 10);
            SetCanCast(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out targetParticle, out _, "shyvana_ult_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
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
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}