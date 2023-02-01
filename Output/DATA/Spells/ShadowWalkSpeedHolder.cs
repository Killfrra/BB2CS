﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShadowWalkSpeedHolder : BBBuffScript
    {
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else if(type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShadowWalkSpeed)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.ShadowWalkSpeed), (ObjAIBase)owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeed(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
        public override void OnDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalkSpeedHolder(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
    }
}