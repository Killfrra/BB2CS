#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonUppercutBlow : BBBuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Crit", 0.5f, owner, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}