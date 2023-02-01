#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingCloneFull : BBBuffScript
    {
        Particle particle; // UNUSED
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "MonkeyKingClone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, default, true, owner, "root", default, target, "root", default, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "MonkeyKingClone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, default, true, owner, "root", default, target, "root", default, false);
            }
        }
    }
}