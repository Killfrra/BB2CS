#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MadredsRazors : BBBuffScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(RandomChance() < 0.2f)
                {
                    if(target is not BaseTurret)
                    {
                        if(target is not Champion)
                        {
                            ApplyDamage(attacker, target, 300, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}