#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_GS_ParticleRed : BBBuffScript
    {
        Particle newName;
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            targetPos = charVars.TargetPos;
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.newName, out _, "pantheon_grandskyfall_tar_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 500, 0, TeamId.TEAM_PURPLE, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.newName, out _, "pantheon_grandskyfall_tar_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 500, 0, TeamId.TEAM_BLUE, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.newName);
        }
    }
}