#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3111 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                AddBuff(attacker, target, new Buffs.Hardening(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MercuryTreads)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MercuryTreads(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}