#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _134 : BBCharScript
    {
        float[] effect0 = {0.033f, 0.066f, 0.1f};
        public override void OnUpdateStats()
        {
            level = talentLevel;
            IncPercentRespawnTimeMod(owner, this.effect0[level]);
        }
    }
}