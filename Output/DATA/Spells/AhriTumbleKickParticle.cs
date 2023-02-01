#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriTumbleKickParticle : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            Particle pH; // UNUSED
            Vector3 targetPos; // UNITIALIZED
            Particle pH2; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out pH, out _, "akali_shadowDance_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out pH2, out _, "irelia_gotasu_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, false, false, false, false);
        }
    }
}