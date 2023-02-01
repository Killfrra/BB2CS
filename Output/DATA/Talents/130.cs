#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _130 : BBCharScript
    {
        float[] effect0 = {0.06f};
        float[] effect1 = {1.5f, 3};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.GhostMovementBonus = this.effect0[level];
            avatarVars.GhostDurationBonus = this.effect1[level];
        }
    }
}