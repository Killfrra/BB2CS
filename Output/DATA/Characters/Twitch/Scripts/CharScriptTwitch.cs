#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTwitch : BBCharScript
    {
        float[] effect0 = {2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 5, 5, 5, 5, 5, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 10, 10, 10};
        public override void SetVarsByLevel()
        {
            charVars.DamageAmount = this.effect0[level];
        }
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
                            float nextBuffVars_DamageAmount;
                            int nextBuffVars_LastCount;
                            AddBuff(attacker, target, new Buffs.DeadlyVenom(), 6, 1, 6.1f, BuffAddType.STACKS_AND_RENEWS, BuffType.POISON, 0, true, false);
                            nextBuffVars_DamageAmount = charVars.DamageAmount;
                            nextBuffVars_LastCount = 1;
                            AddBuff(attacker, target, new Buffs.DeadlyVenom_Internal(nextBuffVars_DamageAmount, nextBuffVars_LastCount), 1, 1, 6.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeadlyVenom_marker(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}