#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _452 : BBCharScript
    {
        float[] effect0 = {0.004f, 0.007f, 0.01f};
        public override void OnUpdateStats()
        {
            float regenPercent;
            float maxMana;
            float regen;
            level = talentLevel;
            regenPercent = this.effect0[level];
            maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            regen = regenPercent * maxMana;
            regen /= 5;
            IncFlatHPRegenMod(owner, regen);
        }
    }
}