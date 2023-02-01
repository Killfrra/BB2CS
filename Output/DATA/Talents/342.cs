#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _342 : BBCharScript
    {
        float[] effect0 = {0.01f, 0.02f, 0.03f};
        public override void OnUpdateActions()
        {
            level = talentLevel;
            avatarVars.MasteryInitiate = true;
            avatarVars.MasteryInitiateAmt = this.effect0[level];
        }
    }
}