#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _106 : BBCharScript
    {
        float[] effect0 = {0.2f, 0.4f, 0.6f};
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