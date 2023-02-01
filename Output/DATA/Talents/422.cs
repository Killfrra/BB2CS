#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _422 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float moveSpeedMod;
            moveSpeedMod = 0.005f * talentLevel;
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}