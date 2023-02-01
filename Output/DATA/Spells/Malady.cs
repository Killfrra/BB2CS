#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Malady : BBBuffScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Miss)
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        AddBuff(attacker, target, new Buffs.MaladyCounter(), 4, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                        AddBuff(attacker, target, new Buffs.MaladySpell(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        ApplyDamage(attacker, target, 20, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                    }
                }
            }
        }
        public override void OnBeingDodged()
        {
            if(attacker is ObjAIBase)
            {
                if(attacker is not BaseTurret)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.MaladyCounter(), 4, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.MaladySpell(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, attacker, 20, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, (ObjAIBase)owner);
                }
            }
        }
    }
}