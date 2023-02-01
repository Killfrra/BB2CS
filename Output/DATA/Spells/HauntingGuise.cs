#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HauntingGuise : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMagicPenetrationMod(owner, 20);
        }
    }
}