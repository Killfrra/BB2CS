#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _124 : BBCharScript
    {
        int[] effect0 = {5, 10};
        int[] effect1 = {30, 30};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.TeleportDelayBonus = 0.5f * talentLevel;
            avatarVars.TeleportCooldownBonus = this.effect0[level];
            avatarVars.PromoteCooldownBonus = this.effect1[level];
        }
    }
}