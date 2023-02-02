#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinDisintegrateDamage : BBBuffScript
    {
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(attacker == caster)
            {
                float hPTotal;
                float hPPercent;
                hPTotal = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                hPPercent = hPTotal * 0.045f;
                damageAmount = Math.Max(hPPercent, damageAmount);
            }
        }
    }
}