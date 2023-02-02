#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BrandSearParticle : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            int brandSkinID;
            Particle a; // UNUSED
            teamID = GetTeamID(attacker);
            brandSkinID = GetSkinID(attacker);
            if(brandSkinID == 3)
            {
                SpellEffectCreate(out a, out _, "BrandBlazeFrost_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "BrandBlaze_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
    }
}