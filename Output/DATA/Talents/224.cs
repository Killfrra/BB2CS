#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _224 : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {10};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(3, ref this.lastTimeExecuted, false))
            {
                level = talentLevel;
                avatarVars.MasteryDemolitionist = true;
                avatarVars.MasteryDemolitionistAmt = this.effect0[level];
            }
        }
    }
}