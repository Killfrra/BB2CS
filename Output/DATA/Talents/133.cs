#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _133 : BBCharScript
    {
        int[] effect0 = {30, 30};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.HealCooldownBonus = this.effect0[level];
        }
    }
}