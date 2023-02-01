#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _129 : BBCharScript
    {
        int[] effect0 = {30, 40};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.RevivePreservationBonus = 400;
            avatarVars.ReviveCooldownBonus = this.effect0[level];
        }
    }
}