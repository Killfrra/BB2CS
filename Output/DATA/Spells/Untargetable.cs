#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Untargetable : BBBuffScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
        }
    }
}