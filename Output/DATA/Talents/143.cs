#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _143 : BBCharScript
    {
        int[] effect0 = {5, 10};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.RallyAPMod = 70;
            avatarVars.RallyDurationBonus = this.effect0[level];
        }
    }
}