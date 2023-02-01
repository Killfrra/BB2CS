#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AsheSpiritOfTheHawkBubble : BBBuffScript
    {
        Vector3 targetPos;
        Region bubbleID;
        public AsheSpiritOfTheHawkBubble(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamID;
            Particle part22; // UNUSED
            Particle part23; // UNUSED
            //RequireVar(this.targetPos);
            targetPos = this.targetPos;
            teamID = GetTeamID(owner);
            this.bubbleID = AddPosPerceptionBubble(teamID, 1000, targetPos, 8, default, false);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out part22, out _, "bowmaster_frostHawk_terminate.troy", default, TeamId.TEAM_BLUE, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true);
                SpellEffectCreate(out part23, out _, "bowmaster_frostHawk_terminate_02.troy", default, TeamId.TEAM_BLUE, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true);
            }
            else
            {
                SpellEffectCreate(out part22, out _, "bowmaster_frostHawk_terminate.troy", default, TeamId.TEAM_PURPLE, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true);
                SpellEffectCreate(out part23, out _, "bowmaster_frostHawk_terminate_02.troy", default, TeamId.TEAM_PURPLE, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
    }
}