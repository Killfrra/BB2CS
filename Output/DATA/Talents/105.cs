#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _105 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float attackSpeedMod;
            attackSpeedMod = 0.01f * talentLevel;
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
    }
}