#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class _4240 : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMagicPenetrationMod(owner, 0.021f);
        }
    }
}