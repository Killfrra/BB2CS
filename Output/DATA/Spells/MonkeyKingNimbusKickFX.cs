#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingNimbusKickFX : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            Particle pH; // UNUSED
            Vector3 targetPos; // UNITIALIZED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out pH, out _, "monkeyKing_Q_unit_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false, false);
        }
    }
}