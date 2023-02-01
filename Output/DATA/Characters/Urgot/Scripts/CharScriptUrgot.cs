#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptUrgot : BBCharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            float aD;
            float bonusDamage;
            if(ExecutePeriodically(0.5f, ref this.lastTime2Executed, true))
            {
                if(owner.IsDead)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.UrgotDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.UrgotDeathParticle), (ObjAIBase)owner, 0);
                    }
                }
                aD = GetFlatPhysicalDamageMod(owner);
                bonusDamage = aD * 0.6f;
                SetSpellToolTipVar(bonusDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}