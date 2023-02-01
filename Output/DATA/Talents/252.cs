#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _252 : BBCharScript
    {
        int[] effect0 = {2, 4, 6};
        public override void OnUpdateStats()
        {
            level = talentLevel;
            IncFlatArmorPenetrationMod(owner, this.effect0[level]);
        }
    }
}