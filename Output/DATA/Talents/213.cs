#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _213 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float abilityPowerBonus;
            abilityPowerBonus = 1 * talentLevel;
            IncFlatMagicDamageMod(owner, abilityPowerBonus);
        }
    }
}