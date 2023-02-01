#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _463 : BBCharScript
    {
        public override void SetVarsByLevel()
        {
            float summonerCooldownBonus;
            summonerCooldownBonus = 0.15f * talentLevel;
            avatarVars.SummonerCooldownBonus = summonerCooldownBonus;
        }
    }
}