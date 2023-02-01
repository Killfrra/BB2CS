#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVCataclysmVisibility : BBBuffScript
    {
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId team;
            team = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(team, 50, target, 10, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
    }
}