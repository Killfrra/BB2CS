#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3146 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(9, ref this.lastTimeExecuted, false))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.HextechGunblade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HextechGunblade)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.HextechGunblade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}