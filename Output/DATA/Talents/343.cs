#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _343 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float cDRPerLevel;
            int champLevel;
            float cDRMod;
            cDRPerLevel = talentLevel * -0.0015f;
            champLevel = GetLevel(owner);
            cDRMod = champLevel * cDRPerLevel;
            IncPercentCooldownMod(owner, cDRMod);
        }
    }
}