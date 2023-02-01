#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _453 : BBCharScript
    {
        float[] effect0 = {-0.02f, -0.04f, -0.06f};
        public override void OnUpdateStats()
        {
            float cooldownMod;
            level = talentLevel;
            cooldownMod = this.effect0[level];
            IncPercentCooldownMod(owner, cooldownMod);
        }
    }
}