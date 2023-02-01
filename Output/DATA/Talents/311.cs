#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _311 : BBCharScript
    {
        public override void SetVarsByLevel()
        {
            avatarVars.DefensiveMastery = talentLevel;
        }
    }
}