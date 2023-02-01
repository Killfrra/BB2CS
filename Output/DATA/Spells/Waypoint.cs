#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Waypoint : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "WaypointBuff",
        };
        Particle waypoint;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.waypoint, out _, "tutorial_waypoint_yellow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.waypoint);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetInvulnerable(owner, true);
            SetForceRenderParticles(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, true);
        }
    }
}