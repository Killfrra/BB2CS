#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BurningEmbers : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, 5);
            IncFlatPhysicalDamageMod(owner, 5);
        }
    }
}