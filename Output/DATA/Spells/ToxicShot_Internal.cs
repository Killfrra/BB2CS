#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ToxicShot_Internal : BBBuffScript
    {
        float damagePerTick;
        float stackingDamagePerTick;
        float lastTimeExecuted;
        public ToxicShot_Internal(float damagePerTick = default, float stackingDamagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
            this.stackingDamagePerTick = stackingDamagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.stackingDamagePerTick);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ToxicShotAttack)) > 0)
                {
                    int count;
                    float stackDamage;
                    float damageAmount;
                    count = GetBuffCountFromAll(owner, nameof(Buffs.ToxicShotAttack));
                    stackDamage = this.stackingDamagePerTick * count;
                    damageAmount = this.damagePerTick + stackDamage;
                    ApplyDamage(attacker, owner, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.14f);
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}