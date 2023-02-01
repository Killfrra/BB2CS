#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3172 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                AddBuff(attacker, target, new Buffs.Hardening(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}