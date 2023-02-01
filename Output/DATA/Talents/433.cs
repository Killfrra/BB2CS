#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _433 : BBCharScript
    {
        float[] effect0 = {0.01f, 0.02f, 0.03f, 0.04f};
        public override void OnUpdateStats()
        {
            float vamp;
            level = talentLevel;
            vamp = this.effect0[level];
            IncPercentSpellVampMod(owner, vamp);
        }
    }
}