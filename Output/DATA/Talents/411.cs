#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _411 : BBCharScript
    {
        public override void SetVarsByLevel()
        {
            avatarVars.UtilityMastery = talentLevel;
        }
    }
}