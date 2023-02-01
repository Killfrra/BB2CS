#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _253 : BBCharScript
    {
        float[] effect0 = {0.0125f, 0.025f, 0.0375f, 0.05f};
        public override void OnUpdateActions()
        {
            float nextBuffVars_PercentMod;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MasteryBlastBuff)) == 0)
            {
                level = talentLevel;
                nextBuffVars_PercentMod = this.effect0[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.MasteryBlastBuff(nextBuffVars_PercentMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}