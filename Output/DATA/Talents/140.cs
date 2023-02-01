#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _140 : BBCharScript
    {
        int[] effect0 = {15, 30};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.FlashCooldownBonus = this.effect0[level];
        }
    }
}