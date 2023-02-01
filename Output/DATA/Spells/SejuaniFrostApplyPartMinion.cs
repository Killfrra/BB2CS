#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniFrostApplyPartMinion : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "SejuaniFrostApply_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}