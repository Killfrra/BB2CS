#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3071 : BBItemScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Miss)
            {
                if(target is ObjAIBase)
                {
                    if(target is BaseTurret)
                    {
                    }
                    else
                    {
                        int nextBuffVars_ArmorReduction;
                        nextBuffVars_ArmorReduction = -15;
                        AddBuff(attacker, target, new Buffs.BlackCleaver(nextBuffVars_ArmorReduction), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
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
                    float nextBuffVars_ArmorReduction;
                    nextBuffVars_ArmorReduction = -15;
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.BlackCleaver(nextBuffVars_ArmorReduction), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                }
            }
        }
    }
}