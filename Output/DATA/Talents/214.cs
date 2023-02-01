#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _214 : BBCharScript
    {
        int[] effect0 = {2, 4};
        public override void OnUpdateActions()
        {
            level = talentLevel;
            avatarVars.MasteryButcher = true;
            avatarVars.MasteryButcherAmt = this.effect0[level];
        }
    }
}