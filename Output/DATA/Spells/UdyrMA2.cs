#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrMA2 : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, 0.15f);
            IncFlatDodgeMod(owner, 0.06f);
        }
    }
}