#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DisableHPRegen : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentHPRegenMod(owner, -1);
        }
    }
}