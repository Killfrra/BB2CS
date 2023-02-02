#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserSyphonParticle : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "mordakaiser_siphonOfDestruction_self.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            SpellEffectCreate(out a, out _, "mordakeiser_hallowedStrike_self_skin.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
        }
    }
}