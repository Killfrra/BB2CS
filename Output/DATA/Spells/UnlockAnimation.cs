#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UnlockAnimation : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner);
        }
    }
}