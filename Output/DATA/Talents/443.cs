#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _443 : BBCharScript
    {
        float[] effect0 = {0.0125f, 0.025f, 0.0375f, 0.05f};
        public override void OnUpdateStats()
        {
            float experienceMod;
            level = talentLevel;
            experienceMod = this.effect0[level];
            IncPercentEXPBonus(owner, experienceMod);
        }
    }
}