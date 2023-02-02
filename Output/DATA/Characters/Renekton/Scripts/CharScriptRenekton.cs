#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptRenekton : BBCharScript
    {
        float lastTime2Executed;
        float renekthonDamage;
        float attackPercentage;
        float bonusDamage;
        float bonusAttackPercentage;
        float rageBonusDamage;
        int[] effect0 = {10, 30, 50, 70, 90};
        int[] effect1 = {15, 45, 75, 105, 135};
        int[] effect2 = {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5};
        float[] effect3 = {1.5f, 1.5f, 1.5f, 1.5f, 1.5f};
        float[] effect4 = {2.25f, 2.25f, 2.25f, 2.25f, 2.25f};
        int[] effect5 = {10, 30, 50, 70, 90};
        int[] effect6 = {15, 45, 75, 105, 135};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                float baseDamage;
                float renektonBonusAD;
                float renekthonTooltip1;
                float renekthonTooltip1b;
                float renekthonTooltip3;
                float renekthonTooltip4;
                float renekthonTooltip2;
                float renekthonTooltip5;
                baseDamage = GetBaseAttackDamage(owner);
                this.renekthonDamage = GetTotalAttackDamage(owner);
                renektonBonusAD = this.renekthonDamage - baseDamage;
                renekthonTooltip1 = renektonBonusAD * 0.8f;
                SetSpellToolTipVar(renekthonTooltip1, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                renekthonTooltip1b = 1.5f * renekthonTooltip1;
                SetSpellToolTipVar(renekthonTooltip1b, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                renekthonTooltip3 = this.renekthonDamage * this.attackPercentage;
                renekthonTooltip3 += this.bonusDamage;
                SetSpellToolTipVar(renekthonTooltip3, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                renekthonTooltip4 = this.renekthonDamage * this.bonusAttackPercentage;
                renekthonTooltip4 += this.rageBonusDamage;
                SetSpellToolTipVar(renekthonTooltip4, 2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                renekthonTooltip2 = renektonBonusAD * 0.9f;
                SetSpellToolTipVar(renekthonTooltip2, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                renekthonTooltip5 = renektonBonusAD * 1.35f;
                SetSpellToolTipVar(renekthonTooltip5, 2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonPredator(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPAR(owner, -99, PrimaryAbilityResourceType.Other);
            charVars.PerPercent = 0.1f;
            this.bonusAttackPercentage = 2.25f;
            this.attackPercentage = 1.5f;
            charVars.RageThreshold = 0.5f;
            charVars.BonusDamage = this.effect0[level];
            charVars.RageBonusDamage = this.effect1[level];
        }
        public override void OnLevelUp()
        {
            level = GetLevel(owner);
            charVars.AutoattackRage = this.effect2[level];
        }
        public override void OnResurrect()
        {
            IncPAR(owner, -99, PrimaryAbilityResourceType.Other);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.attackPercentage = this.effect3[level];
                this.bonusAttackPercentage = this.effect4[level];
                this.bonusDamage = this.effect5[level];
                this.rageBonusDamage = this.effect6[level];
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}