#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptVeigar : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {1, 2, 3, 4, 5};
        int[] effect1 = {1, 1, 1, 1, 1};
        int[] effect2 = {9999, 9999, 9999, 9999, 9999};
        public override void OnUpdateStats()
        {
            float totalBonus;
            //RequireVar(championAPGain);
            //RequireVar(charVars.TotalBonus);
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                totalBonus = 0 + charVars.TotalBonus;
                totalBonus = charVars.APGain + charVars.TotalBonus;
                SetSpellToolTipVar(totalBonus, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
            //RequireVar(charVars.APGain);
            IncFlatMagicDamageMod(owner, charVars.APGain);
        }
        public override void OnKill()
        {
            float championAPGain;
            if(target is Champion)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    championAPGain = this.effect0[level];
                    charVars.APGain += championAPGain;
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.VeigarEquilibrium(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.APGain = 0;
            charVars.TotalBonus = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            int nextBuffVars_BonusAP;
            if(slot == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_BonusAP = this.effect1[level];
                charVars.MaxBonus = this.effect2[level];
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}