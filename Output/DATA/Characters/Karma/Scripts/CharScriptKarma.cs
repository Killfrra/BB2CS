#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptKarma : BBCharScript
    {
        float lastTime2Executed;
        int[] effect0 = {30, 30, 30, 30, 30, 30, 25, 25, 25, 25, 25, 25, 20, 20, 20, 20, 20, 20};
        int[] effect1 = {15, 14, 13, 12, 11, 10};
        public override void OnUpdateActions()
        {
            float mantraTimerCooldown;
            float cooldownMod;
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                level = GetLevel(owner);
                mantraTimerCooldown = this.effect0[level];
                cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.MantraTimerCooldown = mantraTimerCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.MantraTimerCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int spellSlot;
            float cooldownStat;
            float baseCooldown;
            float multiplier;
            float newCooldown;
            spellSlot = GetSpellSlot();
            spellName = GetSpellName();
            if(spellSlot == 3)
            {
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaChakra)) > 0)
                {
                    if(spellName == nameof(Spells.KarmaSoulShieldC))
                    {
                        cooldownStat = GetPercentCooldownMod(owner);
                        baseCooldown = 10;
                        multiplier = 1 + cooldownStat;
                        newCooldown = multiplier * baseCooldown;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                        SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KarmaChakra), 1);
                    }
                    else if(spellName == nameof(Spells.KarmaSpiritBondC))
                    {
                        cooldownStat = GetPercentCooldownMod(owner);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        baseCooldown = this.effect1[level];
                        multiplier = 1 + cooldownStat;
                        newCooldown = multiplier * baseCooldown;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                        SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KarmaChakra), 1);
                    }
                    else if(spellName == nameof(Spells.KarmaHeavenlyWaveC))
                    {
                        cooldownStat = GetPercentCooldownMod(owner);
                        baseCooldown = 6;
                        multiplier = 1 + cooldownStat;
                        newCooldown = multiplier * baseCooldown;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                        SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KarmaChakra), 1);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            charVars.MantraTimerCooldown = 25;
            IncSpellLevel((ObjAIBase)owner, 3, SpellSlotType.SpellSlots);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaChakraCharge(), 3, 2, charVars.MantraTimerCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaOneMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaTranscendence(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaChakraCharge(), 3, 3, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaOneMantraParticle)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaOneMantraParticle), (ObjAIBase)owner, 0);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaTwoMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}