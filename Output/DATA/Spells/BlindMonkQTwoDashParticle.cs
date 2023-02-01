#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkQTwoDashParticle : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            Particle pH; // UNUSED
            Vector3 targetPos; // UNITIALIZED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out pH, out _, "blindMonk_Q_resonatingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
            SpellEffectCreate(out pH, out _, "blindMonk_Q_resonatingStrike_tar_blood.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
            SpellEffectCreate(out pH, out _, "blindmonk_resonatingstrike_tar_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
        }
    }
}