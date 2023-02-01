#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _243 : BBCharScript
    {
        float[] effect0 = {0.25f, 0.5f, 0.75f, 1};
        public override void OnUpdateStats()
        {
            int ownerLevel;
            float bonusDamage;
            float totalBonusDamage;
            ownerLevel = GetLevel(owner);
            level = talentLevel;
            bonusDamage = this.effect0[level];
            totalBonusDamage = bonusDamage * ownerLevel;
            IncFlatMagicDamageMod(owner, totalBonusDamage);
        }
    }
}