#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _148 : BBCharScript
    {
        public override void SetVarsByLevel()
        {
            avatarVars.StifleDurationBonus = 0.5f;
            avatarVars.StifleCooldownBonus = 10;
        }
    }
}