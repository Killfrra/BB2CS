#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BootsOfMobilityDebuff : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, -60);
        }
    }
}