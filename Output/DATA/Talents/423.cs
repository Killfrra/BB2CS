#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _423 : BBCharScript
    {
        float[] effect0 = {0.2f, 0.4f, 0.6f};
        public override void OnUpdateStats()
        {
            float regenMod;
            level = talentLevel;
            regenMod = this.effect0[level];
            IncFlatPARRegenMod(owner, regenMod, PrimaryAbilityResourceType.MANA);
        }
    }
}