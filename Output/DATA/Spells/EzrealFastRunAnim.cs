#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EzrealFastRunAnim : BBBuffScript
    {
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Run2", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
        }
    }
}