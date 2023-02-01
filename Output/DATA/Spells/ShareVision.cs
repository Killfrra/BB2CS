#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShareVision : BBBuffScript
    {
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId casterTeamID;
            casterTeamID = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(casterTeamID, 0, owner, 20000, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
    }
}