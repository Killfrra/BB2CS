#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptKayle : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {20, 30, 40, 50, 60};
        float[] effect1 = {1.06f, 1.07f, 1.08f, 1.09f, 1.1f};
        public override void OnUpdateActions()
        {
            float kayleAP;
            float damageMod;
            float attackDamage;
            float baseAD;
            float totalAD;
            float bonusAD;
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, true))
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 0)
                {
                    level = 1;
                }
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                kayleAP = GetFlatMagicDamageMod(owner);
                kayleAP *= 0.2f;
                damageMod = this.effect0[level];
                attackDamage = kayleAP + damageMod;
                baseAD = GetBaseAttackDamage(owner);
                totalAD = GetTotalAttackDamage(owner);
                bonusAD = totalAD - baseAD;
                bonusAD *= 1;
                SetSpellToolTipVar(attackDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                SetSpellToolTipVar(bonusAD, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is Champion)
            {
                AddBuff(attacker, target, new Buffs.JudicatorHolyFervorDebuff(), 5, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
            }
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float damagePercent;
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.JudicatorReckoning)) > 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    damagePercent = this.effect1[level];
                    damageAmount *= damagePercent;
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.JudicatorHolyFervor(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}