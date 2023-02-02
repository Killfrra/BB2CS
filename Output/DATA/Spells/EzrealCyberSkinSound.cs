#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EzrealCyberSkinSound : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            int ezrealSkinID;
            teamID = GetTeamID(attacker);
            ezrealSkinID = GetSkinID(attacker);
            if(ezrealSkinID == 5)
            {
                Particle a; // UNUSED
                SpellEffectCreate(out a, out _, "Ezreal_cyberezreal_gamestart.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
    }
}