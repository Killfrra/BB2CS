#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _118 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float cooldownMod;
            cooldownMod = -0.02f * talentLevel;
            IncPercentCooldownMod(owner, cooldownMod);
        }
    }
}