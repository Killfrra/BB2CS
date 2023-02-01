#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeapStrikeSpeed : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.2f);
            SetCanAttack(owner, false);
        }
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
        }
    }
}