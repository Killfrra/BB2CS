#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinChannelVision : BBBuffScript
    {
        Region bubbleID;
        Region bubbleID2;
        Region bubbleID3;
        Region bubbleID4;
        public override void OnActivate()
        {
            TeamId orderTeam;
            TeamId chaosTeam;
            orderTeam = TeamId.TEAM_BLUE;
            chaosTeam = TeamId.TEAM_PURPLE;
            this.bubbleID = AddUnitPerceptionBubble(orderTeam, 400, owner, 20, default, default, false);
            this.bubbleID2 = AddUnitPerceptionBubble(orderTeam, 50, owner, 20, default, default, true);
            this.bubbleID3 = AddUnitPerceptionBubble(chaosTeam, 400, owner, 20, default, default, false);
            this.bubbleID4 = AddUnitPerceptionBubble(chaosTeam, 50, owner, 20, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
            RemovePerceptionBubble(this.bubbleID3);
            RemovePerceptionBubble(this.bubbleID4);
        }
    }
}