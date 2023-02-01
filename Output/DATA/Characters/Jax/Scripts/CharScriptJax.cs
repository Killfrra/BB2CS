#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptJax : BBCharScript
    {
        int[] effect0 = {25, 45, 65};
        public override void OnUpdateActions()
        {
            float totalAD;
            float baseAD;
            float bonusAD;
            float bonusAD1a;
            float attackDamage; // UNITIALIZED
            float bonusAD1b;
            float bonusAD2;
            float bonusADAP;
            float bonusAP;
            float multiplier;
            totalAD = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            bonusAD1a = bonusAD * 1;
            SetSpellToolTipVar(bonusAD1a, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            bonusAD1b = attackDamage * 1;
            SetSpellToolTipVar(bonusAD1b, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            bonusAD2 = bonusAD * 0.8f;
            SetSpellToolTipVar(bonusAD2, 2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level == 0)
            {
                level = 1;
            }
            bonusADAP = this.effect0[level];
            totalAD = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            bonusAP = GetFlatMagicDamageMod(owner);
            multiplier = 0.2f;
            bonusAD *= multiplier;
            bonusAP *= multiplier;
            bonusAP += bonusADAP;
            bonusAD += bonusADAP;
            SetSpellToolTipVar(bonusAP, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            SetSpellToolTipVar(bonusAD, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxPassive(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.NumSwings = 0;
            charVars.LastHitTime = 0;
            charVars.UltStacks = 6;
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}