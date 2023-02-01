#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _132 : BBCharScript
    {
        float[] effect0 = {0.02f, 0.03f, 0.04f};
        public override void OnUpdateStats()
        {
            float regenPercent;
            level = talentLevel;
            regenPercent = this.effect0[level];
            IncPercentPARRegenMod(owner, regenPercent, PrimaryAbilityResourceType.MANA);
            IncPercentHPRegenMod(owner, regenPercent);
        }
    }
}