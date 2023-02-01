#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3123 : BBItemScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is not BaseTurret)
            {
                if(target is ObjAIBase)
                {
                    if(owner is not Champion)
                    {
                        attacker = GetPetOwner((Pet)owner);
                    }
                    AddBuff(attacker, target, new Buffs.Mourning(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false);
                }
            }
        }
    }
}