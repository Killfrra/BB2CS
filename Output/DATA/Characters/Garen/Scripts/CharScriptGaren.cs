#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptGaren : BBCharScript
    {
        float lastTime2Executed;
        int[] effect0 = {15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32};
        float[] effect1 = {0.5f, 0.5f, 0.5f, 0.5f, 0.5f};
        int[] effect2 = {25, 25, 25, 25, 25};
        float[] effect3 = {0.5f, 0.5f, 0.5f, 0.5f, 0.5f};
        public override void OnUpdateActions()
        {
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float spell3Display;
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                totalDamage = GetTotalAttackDamage(owner);
                baseDamage = GetBaseAttackDamage(owner);
                bonusDamage = totalDamage - baseDamage;
                spell3Display = bonusDamage * 1.4f;
                SetSpellToolTipVar(spell3Display, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.RegenMod = this.effect0[level];
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string name;
            name = GetSpellName();
            if(name == nameof(Spells.GarenJustice))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.GarenJusticePreCast(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecouperateOn(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.CommandBonus = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            float nextBuffVars_BonusArmor;
            float nextBuffVars_BonusMR;
            if(slot == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    charVars.TotalBonus = 0;
                    charVars.CommandReady = 0;
                    nextBuffVars_BonusArmor = this.effect1[level];
                    charVars.MaxBonus = this.effect2[level];
                    nextBuffVars_BonusMR = this.effect3[level];
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenCommandKill(nextBuffVars_BonusArmor, nextBuffVars_BonusMR), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}