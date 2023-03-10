#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3093 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(9, ref this.lastTimeExecuted, false))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AvariceBlade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class _3093 : BBBuffScript
    {
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AvariceBlade)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AvariceBlade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}