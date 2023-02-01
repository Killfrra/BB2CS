#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CaitlynAceintheHoleVisibility : BBBuffScript
    {
        Region bubbleID;
        public CaitlynAceintheHoleVisibility(Region bubbleID = default)
        {
            this.bubbleID = bubbleID;
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
        public override void OnActivate()
        {
            //RequireVar(this.bubbleID);
        }
    }
}