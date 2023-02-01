#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class _4233 : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentArmorPenetrationMod(owner, 0.017f);
        }
    }
}