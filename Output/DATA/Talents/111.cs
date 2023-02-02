#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _111 : BBCharScript
    {
        float[] effect0 = {0.01333f, 0.02667f, 0.04f};
        public override void OnUpdateActions()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Ardor)) == 0)
            {
                float nextBuffVars_PercentMod;
                level = talentLevel;
                nextBuffVars_PercentMod = this.effect0[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.Ardor(nextBuffVars_PercentMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}