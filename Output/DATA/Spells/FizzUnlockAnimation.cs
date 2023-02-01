#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzUnlockAnimation : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnActivate()
        {
            SetGhosted(owner, true);
        }
        public override void OnUpdateActions()
        {
            SetGhosted(owner, true);
        }
    }
}