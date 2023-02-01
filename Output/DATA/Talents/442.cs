#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _442 : BBCharScript
    {
        int[] effect0 = {20, 40};
        public override void OnUpdateActions()
        {
            int nextBuffVars_GoldAmount;
            level = talentLevel;
            nextBuffVars_GoldAmount = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.MasteryHoardBuff(nextBuffVars_GoldAmount), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}