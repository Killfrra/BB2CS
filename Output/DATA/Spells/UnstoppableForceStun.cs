#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UnstoppableForceStun : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UnstoppableForceStun",
            BuffTextureName = "Malphite_UnstoppableForce.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            TeamId teamID;
            Particle targetParticle; // UNUSED
            Vector3 position;
            attacker = SetBuffCasterUnit();
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out targetParticle, out _, "UnstoppableForce_stun.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            position = GetRandomPointInAreaUnit(owner, 125, 75);
            Move(owner, position, 100, 20, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 100);
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
        }
    }
}