#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EvelynnMaliceandSpiteTickDamage : BBBuffScript
    {
        float tickDamage;
        public EvelynnMaliceandSpiteTickDamage(float tickDamage = default)
        {
            this.tickDamage = tickDamage;
        }
        public override void OnActivate()
        {
            ObjAIBase caster;
            //RequireVar(this.tickDamage);
            charVars.DoOnce = false;
            caster = SetBuffCasterUnit();
            ApplyDamage(caster, owner, this.tickDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, caster);
        }
        public override void OnUpdateStats()
        {
            charVars.DoOnce = true;
        }
        public override void OnDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if(!charVars.DoOnce)
                {
                    charVars.DoOnce = true;
                }
            }
        }
    }
}