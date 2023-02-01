#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RevealWard : BBBuffScript
    {
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId myTeamID;
            TeamId bubbleTeamID;
            myTeamID = GetTeamID(attacker);
            if(myTeamID == TeamId.TEAM_BLUE)
            {
                bubbleTeamID = 200;
            }
            else
            {
                bubbleTeamID = 100;
            }
            this.bubbleID = AddUnitPerceptionBubble(bubbleTeamID, 5, owner, 5, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
    }
}