#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _242 : BBCharScript
    {
        float[] effect0 = {0.01f, 0.02f, 0.03f};
        public override void OnUpdateStats()
        {
            level = talentLevel;
            IncPercentLifeStealMod(owner, this.effect0[level]);
        }
    }
}