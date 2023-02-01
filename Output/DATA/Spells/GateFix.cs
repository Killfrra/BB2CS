#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GateFix : BBBuffScript
    {
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
        }
    }
}