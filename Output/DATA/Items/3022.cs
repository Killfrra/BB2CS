#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3022 : BBItemScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
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
                            if(IsRanged(owner))
                            {
                                AddBuff((ObjAIBase)target, target, new Buffs.Internal_30Slow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                            }
                            else
                            {
                                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                                {
                                    AddBuff((ObjAIBase)target, target, new Buffs.Internal_30Slow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                                }
                                else
                                {
                                    AddBuff((ObjAIBase)target, target, new Buffs.Internal_40Slow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                                }
                            }
                            AddBuff((ObjAIBase)owner, target, new Buffs.ItemSlow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                        }
                    }
                }
            }
        }
    }
}