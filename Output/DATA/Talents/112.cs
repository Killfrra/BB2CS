#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _112 : BBCharScript
    {
        float[] effect0 = {0.00066f, 0.00133f, 0.002f};
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.StrengthOfSpirit)) == 0)
            {
                float nextBuffVars_multiplier;
                level = talentLevel;
                nextBuffVars_multiplier = this.effect0[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.StrengthOfSpirit(nextBuffVars_multiplier), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}