#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTeemo : BBCharScript
    {
        float lastTime2Executed;
        int[] effect0 = {35, 31, 27};
        int[] effect1 = {6, 6, 6, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14, 14, 14, 16, 16, 16};
        public override void OnUpdateActions()
        {
            float mushroomCooldown;
            float cooldownMod;
            float mushroomCooldownNL;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ToxicShot)) > 0)
            {
            }
            else
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ToxicShot(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 0)
                {
                    level = 1;
                }
                mushroomCooldown = this.effect0[level];
                cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.MushroomCooldown = mushroomCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.MushroomCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                mushroomCooldownNL = mushroomCooldown - 4;
                mushroomCooldownNL *= cooldownMod;
                SetSpellToolTipVar(mushroomCooldownNL, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.TrailDuration = this.effect1[level];
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Camouflage(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0.1f, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 3)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.TeemoMushrooms(), 4, 2, charVars.MushroomCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                }
            }
            if(slot == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.TeemoMoveQuickPassive(), 1, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}