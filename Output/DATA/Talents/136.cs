#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _136 : BBCharScript
    {
        float[] effect0 = {0.5f, 1};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.ExhaustArmorMod = -10;
            avatarVars.ExhaustDurationBonus = this.effect0[level];
        }
    }
}