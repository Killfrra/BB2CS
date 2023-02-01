#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _131 : BBCharScript
    {
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.BoostCooldownBonus = 20;
        }
    }
}