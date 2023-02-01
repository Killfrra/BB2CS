#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3067 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(9, ref this.lastTimeExecuted, false))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Kindlegem(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Kindlegem)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Kindlegem(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}