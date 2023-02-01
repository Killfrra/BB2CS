#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptAkali : BBCharScript
    {
        float lastTime2Executed;
        float akaliDamageVar;
        int tickTock; // UNUSED
        int[] effect0 = {25, 20, 15};
        int[] effect1 = {20, 15, 10};
        public override void OnUpdateActions()
        {
            float akaliDamage1;
            float danceTimerCooldown;
            float cooldownMod;
            float danceTimerCooldownNL;
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                this.akaliDamageVar = GetTotalAttackDamage(owner);
                akaliDamage1 = this.akaliDamageVar * 0.6f;
                SetSpellToolTipVar(akaliDamage1, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 0)
                {
                    level = 1;
                }
                danceTimerCooldown = this.effect0[level];
                cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.DanceTimerCooldown = danceTimerCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.DanceTimerCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                danceTimerCooldownNL = danceTimerCooldown - 5;
                danceTimerCooldownNL *= cooldownMod;
                SetSpellToolTipVar(danceTimerCooldownNL, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnKill()
        {
            int count;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance)) > 0)
            {
                if(target is Champion)
                {
                    count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
                    if(count >= 4)
                    {
                    }
                    else if(count >= 3)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 0, BuffAddType.STACKS_AND_CONTINUE, BuffType.COUNTER, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnAssist()
        {
            int count;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance)) > 0)
            {
                if(target is Champion)
                {
                    count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
                    if(count >= 4)
                    {
                    }
                    else if(count >= 3)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 0, BuffAddType.STACKS_AND_CONTINUE, BuffType.COUNTER, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliTwinDisciplines(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IsNinja(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            this.akaliDamageVar;
            charVars.VampPercent = 0;
        }
        public override void OnResurrect()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.tickTock = this.effect1[level];
            if(level > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 4, 4, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 3)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 4, 2, 25, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                }
            }
        }
    }
}