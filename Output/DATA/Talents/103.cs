#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _103 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float criticalMod;
            criticalMod = 0.0066f * talentLevel;
            IncFlatCritChanceMod(owner, criticalMod);
        }
    }
}