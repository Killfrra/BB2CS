#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptCorki : BBCharScript
    {
        float lastTimeExecuted;
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            float chargeCooldown;
            float cooldownMod;
            float totalDamage;
            float baseAD;
            float bonusDamage;
            if(ExecutePeriodically(0.4f, ref this.lastTimeExecuted, false))
            {
                if(owner.IsDead)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CorkiDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CorkiDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.CorkiDeathParticle), (ObjAIBase)owner, 0);
                    }
                }
            }
            if(ExecutePeriodically(2, ref this.lastTime2Executed, true))
            {
                chargeCooldown = 10;
                cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.ChargeCooldown = chargeCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.ChargeCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                totalDamage = GetTotalAttackDamage(owner);
                baseAD = GetBaseAttackDamage(owner);
                bonusDamage = totalDamage - baseAD;
                bonusDamage *= 0.2f;
                bonusDamage *= 2;
                SetSpellToolTipVar(bonusDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnActivate()
        {
            charVars.BarrageCounter = 0;
            AddBuff((ObjAIBase)owner, owner, new Buffs.RapidReload(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 20000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                SpellBuffClear(owner, nameof(Buffs.MissileBarrage));
                AddBuff((ObjAIBase)owner, owner, new Buffs.MissileBarrage(), 8, 8, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 3)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissileBarrage(), 8, 2, charVars.ChargeCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CorkiMissileBarrageNC(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}