#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOriannaBall : BBCharScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            Champion caster;
            Vector3 nextBuffVars_MyPosition;
            Vector3 myPosition;
            teamID = GetTeamID(owner);
            caster = GetChampionBySkinName("Orianna", teamID ?? TeamId.TEAM_UNKNOWN);
            AddBuff(caster, owner, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(caster, owner, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            myPosition = GetUnitPosition(owner);
            nextBuffVars_MyPosition = myPosition;
            AddBuff((ObjAIBase)owner, caster, new Buffs.OriannaBallTracker(nextBuffVars_MyPosition), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}