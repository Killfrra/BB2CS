#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxMaliceCannonBeam : BBBuffScript
    {
        Region a;
        Region b;
        Particle particleID;
        Particle particleID2;
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            TeamId teamChaosID;
            TeamId teamOrderID;
            Vector3 beam1; // UNUSED
            Vector3 beam2; // UNUSED
            Vector3 beam3; // UNUSED
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                teamChaosID = TeamId.TEAM_PURPLE;
                this.a = AddUnitPerceptionBubble(teamChaosID, 10, owner, 2, default, default, false);
                this.b = AddUnitPerceptionBubble(teamChaosID, 10, attacker, 2, default, default, false);
            }
            else
            {
                teamOrderID = TeamId.TEAM_BLUE;
                this.a = AddUnitPerceptionBubble(teamOrderID, 10, owner, 2, default, default, false);
                this.b = AddUnitPerceptionBubble(teamOrderID, 10, attacker, 2, default, default, false);
            }
            beam1 = GetPointByUnitFacingOffset(owner, 550, 0);
            beam2 = GetPointByUnitFacingOffset(owner, 1650, 0);
            beam3 = GetPointByUnitFacingOffset(owner, 2750, 0);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particleID, out _, "LuxMaliceCannon_beam.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "top", default, attacker, "top", default, false);
                SpellEffectCreate(out this.particleID2, out _, "LuxMaliceCannon_beam.troy", default, TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "top", default, attacker, "top", default, false);
            }
            else
            {
                SpellEffectCreate(out this.particleID, out _, "LuxMaliceCannon_beam.troy", default, TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "top", default, attacker, "top", default, false);
                SpellEffectCreate(out this.particleID2, out _, "LuxMaliceCannon_beam.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "top", default, attacker, "top", default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.particleID2);
            RemovePerceptionBubble(this.a);
            RemovePerceptionBubble(this.b);
        }
    }
}