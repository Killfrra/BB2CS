#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorAugmentR : BBBuffScript
    {
        public override void OnActivate()
        {
            IncPermanentPercentCooldownMod(owner, -0.1f);
        }
    }
}