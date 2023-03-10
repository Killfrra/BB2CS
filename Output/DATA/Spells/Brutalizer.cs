#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Brutalizer : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
            IncFlatArmorPenetrationMod(owner, 15);
        }
    }
}