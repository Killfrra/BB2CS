#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3106 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(9, ref this.lastTimeExecuted, true))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WriggleLantern)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MadredsRazors(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    SpellBuffRemove(owner, nameof(Buffs.MadredsRazors), (ObjAIBase)owner);
                }
            }
        }
    }
}