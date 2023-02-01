#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _432 : BBCharScript
    {
        float[] effect0 = {0.5f, 1, 1.5f, 2};
        public override void OnUpdateStats()
        {
            float greed;
            level = talentLevel;
            greed = this.effect0[level];
            IncFlatGoldPer10Mod(owner, greed);
        }
    }
}