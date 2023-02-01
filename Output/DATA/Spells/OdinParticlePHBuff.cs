#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinParticlePHBuff : BBBuffScript
    {
        Particle particle; // UNUSED
        Particle particle2; // UNUSED
        public override void OnActivate()
        {
            Vector3 ownerPos; // UNUSED
            Vector3 castPos;
            TeamId teamID;
            ownerPos = GetUnitPosition(owner);
            castPos = GetPointByUnitFacingOffset(owner, 150, 0);
            teamID = GetTeamID(owner);
            SetCanMove(owner, false);
            SetForceRenderParticles(owner, true);
            SetTargetable(owner, false);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "OdinDONTSHIPTHIS_Green.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
                SpellEffectCreate(out this.particle2, out _, "OdinDONTSHIPTHIS_Red.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "OdinDONTSHIPTHIS_Red.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_BLUE, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
                SpellEffectCreate(out this.particle2, out _, "OdinDONTSHIPTHIS_Green.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
            }
        }
    }
}