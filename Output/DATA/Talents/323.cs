#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _323 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float hPRegenBonus;
            hPRegenBonus = 0.2f * talentLevel;
            IncFlatHPRegenMod(owner, hPRegenBonus);
        }
    }
}