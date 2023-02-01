#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _115 : BBCharScript
    {
        float[] effect0 = {0.0125f, 0.025f, 0.0375f, 0.05f};
        public override void OnUpdateStats()
        {
            float manaMod;
            level = talentLevel;
            manaMod = this.effect0[level];
            IncPercentPARPoolMod(owner, manaMod, PrimaryAbilityResourceType.MANA);
        }
    }
}