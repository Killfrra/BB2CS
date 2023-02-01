#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _1062 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            int nextBuffVars_HealthVar;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                nextBuffVars_HealthVar = 200;
                AddBuff((ObjAIBase)owner, owner, new Buffs.DoranT2Health(nextBuffVars_HealthVar), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}