#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _412 : BBCharScript
    {
        float[] effect0 = {0.04f, 0.07f, 0.1f};
        public override void OnUpdateStats()
        {
            level = talentLevel;
            IncPercentRespawnTimeMod(owner, this.effect0[level]);
        }
    }
}