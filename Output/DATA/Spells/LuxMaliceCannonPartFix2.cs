#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxMaliceCannonPartFix2 : BBBuffScript
    {
        public override void OnActivate()
        {
            Vector3 ownerPos; // UNUSED
            TeamId teamID;
            Vector3 beam1;
            Vector3 beam2;
            Vector3 beam3;
            Minion other1;
            Minion other2;
            Minion other3;
            ownerPos = GetUnitPosition(owner);
            teamID = GetTeamID(owner);
            beam1 = GetPointByUnitFacingOffset(owner, 1884, 0);
            beam2 = GetPointByUnitFacingOffset(owner, 2826, 0);
            beam3 = GetPointByUnitFacingOffset(owner, 2475, 0);
            other1 = SpawnMinion("hiu", "TestCube", "idle.lua", beam1, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, default, false, (Champion)owner);
            other2 = SpawnMinion("hiu", "TestCube", "idle.lua", beam2, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, default, false, (Champion)owner);
            other3 = SpawnMinion("hiu", "TestCube", "idle.lua", beam3, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, default, false, (Champion)owner);
            LinkVisibility(other1, attacker);
            LinkVisibility(other2, attacker);
            LinkVisibility(other3, attacker);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}