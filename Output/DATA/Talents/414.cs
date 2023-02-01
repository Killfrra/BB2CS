#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _414 : BBCharScript
    {
        public override void OnUpdateActions()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MasteryImprovedRecallBuff)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MasteryImprovedRecallBuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}