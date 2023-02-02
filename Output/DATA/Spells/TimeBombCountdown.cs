#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TimeBombCountdown : BBBuffScript
    {
        float tickDamage;
        int activations;
        public TimeBombCountdown(float tickDamage = default)
        {
            this.tickDamage = tickDamage;
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.activations == 1)
            {
                if(damageAmount > 0)
                {
                    if(damageAmount <= 10)
                    {
                        ObjAIBase caster;
                        damageAmount = this.tickDamage;
                        this.activations = 0;
                        caster = SetBuffCasterUnit();
                        SpellBuffRemove(owner, nameof(Buffs.TimeBombCountdown), caster);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            this.activations = 1;
            //RequireVar(this.tickDamage);
        }
    }
}