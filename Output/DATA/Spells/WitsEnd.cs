﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WitsEnd : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WitsEndBuff",
            BuffTextureName = "3091_Wits_End.dds",
        };
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
                            AddBuff((ObjAIBase)owner, owner, new Buffs.WitsEndBuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff((ObjAIBase)owner, owner, new Buffs.WitsEndCounter(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.WitsEndBuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff((ObjAIBase)owner, owner, new Buffs.WitsEndCounter(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                            ApplyDamage(attacker, target, 42, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                        }
                    }
                    else
                    {
                        if(default is not BaseTurret)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.WitsEndBuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff((ObjAIBase)owner, owner, new Buffs.WitsEndCounter(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}