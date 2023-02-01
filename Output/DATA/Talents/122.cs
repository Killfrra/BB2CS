#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _122 : BBCharScript
    {
        int[] effect0 = {5, 10};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.ClairvoyanceDurationBonus = 4;
            avatarVars.ClairvoyanceCooldownBonus = this.effect0[level];
        }
    }
}