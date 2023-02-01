#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _108 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float healthMod;
            healthMod = 12 * talentLevel;
            IncFlatHPPoolMod(owner, healthMod);
        }
    }
}