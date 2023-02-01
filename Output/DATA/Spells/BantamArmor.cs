#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BantamArmor : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, 150);
        }
    }
}