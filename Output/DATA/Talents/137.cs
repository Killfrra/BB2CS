#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _137 : BBCharScript
    {
        public override void OnUpdateActions()
        {
            int nextBuffVars_Level;
            nextBuffVars_Level = talentLevel;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OffensiveMasteryBuff)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OffensiveMasteryBuff(nextBuffVars_Level), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}