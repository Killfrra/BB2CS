#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _223 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float cooldownMod;
            cooldownMod = -0.01f * talentLevel;
            IncPercentCooldownMod(owner, cooldownMod);
        }
    }
}