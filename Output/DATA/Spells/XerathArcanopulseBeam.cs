#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathArcanopulseBeam : BBBuffScript
    {
        Particle particleID; // UNUSED
        Particle particleID2; // UNUSED
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(owner);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particleID, out this.particleID2, "XerathR_beam_warning_green.troy", "XerathR_beam_warning_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 550, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particleID, out this.particleID2, "XerathR_beam_warning_green.troy", "XerathR_beam_warning_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 550, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, true, false, false, false, false);
            }
        }
    }
}