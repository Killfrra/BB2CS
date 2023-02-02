#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _262 : BBCharScript
    {
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                if(target is ObjAIBase)
                {
                    if(target is BaseTurret)
                    {
                    }
                    else
                    {
                        float healthPerc;
                        healthPerc = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                        if(healthPerc <= 0.4f)
                        {
                            damageAmount *= 1.06f;
                        }
                    }
                }
            }
        }
    }
}