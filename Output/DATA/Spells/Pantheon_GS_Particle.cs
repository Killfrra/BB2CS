#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_GS_Particle : BBBuffScript
    {
        Particle newName;
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            teamOfOwner = GetTeamID(owner);
            targetPos = charVars.TargetPos;
            SpellEffectCreate(out this.newName, out _, "pantheon_grandskyfall_tar_green.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 500, 0, TeamId.TEAM_UNKNOWN, teamOfOwner, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.newName);
        }
    }
}