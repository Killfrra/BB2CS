#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _104 : BBCharScript
    {
        float[] effect0 = {0.033f, 0.066f, 0.1f};
        public override void OnUpdateStats()
        {
            level = talentLevel;
            IncFlatCritDamageMod(owner, this.effect0[level]);
        }
    }
}