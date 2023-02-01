#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3154 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(9, ref this.lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.WriggleLantern(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MadredsRazors)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.MadredsRazors), (ObjAIBase)owner);
                }
            }
        }
    }
}