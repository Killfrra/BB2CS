#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3068 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                if(!owner.IsDead)
                {
                    AddBuff(attacker, owner, new Buffs.SunfireCloakParticle(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
        }
        public override void OnDeath()
        {
            SpellBuffRemove(owner, nameof(Buffs.SunfireCloakParticle), (ObjAIBase)owner);
        }
    }
}