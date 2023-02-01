#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCaptureImmobile : BBBuffScript
    {
        public override void OnActivate()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            IssueOrder(owner, OrderType.OrderNone, default, owner);
        }
    }
}