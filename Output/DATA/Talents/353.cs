#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _353 : BBCharScript
    {
        int[] effect0 = {8, 16, 24};
        public override void OnUpdateActions()
        {
            level = talentLevel;
            avatarVars.MasteryBounty = true;
            avatarVars.MasteryBountyAmt = this.effect0[level];
        }
    }
}