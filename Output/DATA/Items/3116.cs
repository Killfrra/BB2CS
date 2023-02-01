﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3116 : BBItemScript
    {
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(owner.Team != target.Team)
                    {
                        if(damageSource == DamageSource.DAMAGE_SOURCE_SPELL)
                        {
                            AddBuff((ObjAIBase)target, target, new Buffs.Internal_35Slow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff(attacker, target, new Buffs.ItemSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                        }
                        else if(damageSource == DamageSource.DAMAGE_SOURCE_SPELLAOE)
                        {
                            AddBuff((ObjAIBase)target, target, new Buffs.Internal_15Slow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff(attacker, target, new Buffs.ItemSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                        }
                        else if(damageSource == DamageSource.DAMAGE_SOURCE_SPELLPERSIST)
                        {
                            AddBuff((ObjAIBase)target, target, new Buffs.Internal_15Slow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff(attacker, target, new Buffs.ItemSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}