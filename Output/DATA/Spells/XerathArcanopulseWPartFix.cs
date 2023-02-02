#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathArcanopulseWPartFix : BBBuffScript
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
            beam1 = GetPointByUnitFacingOffset(owner, -145, 0);
            beam2 = GetPointByUnitFacingOffset(owner, 250, 0);
            beam3 = GetPointByUnitFacingOffset(owner, 550, 0);
            other1 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam1, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, false, false);
            other2 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam2, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, false, false);
            other3 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam3, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, false, false);
            LinkVisibility(other1, owner);
            LinkVisibility(other1, other2);
            LinkVisibility(other2, other3);
            LinkVisibility(attacker, other3);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}