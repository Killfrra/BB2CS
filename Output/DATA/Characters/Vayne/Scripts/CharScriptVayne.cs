#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptVayne : BBCharScript
    {
        float[] effect0 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        public override void OnUpdateStats()
        {
            bool hunt;
            bool visible;
            float speedBoost;
            hunt = false;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                if(IsInFront(owner, unit))
                {
                    visible = CanSeeTarget(owner, unit);
                    if(visible)
                    {
                        hunt = true;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.VayneHunted(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.VayneHunted)) > 0)
                    {
                        hunt = true;
                    }
                }
            }
            if(hunt)
            {
                speedBoost = 30;
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
                {
                    speedBoost *= 3;
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisitionSpeedPart)) == 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPartNormal), (ObjAIBase)owner, 0);
                        AddBuff((ObjAIBase)owner, owner, new Buffs.VayneInquisitionSpeedPart(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VayneInquisitionSpeedPartNormal(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPart), (ObjAIBase)owner, 0);
                }
                IncFlatMovementSpeedMod(owner, speedBoost);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPart), (ObjAIBase)owner, 0);
                SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPartNormal), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            float aD;
            float bonusDamage2;
            float damage;
            float spear;
            float spearDamage;
            aD = GetFlatPhysicalDamageMod(owner);
            bonusDamage2 = aD * 0.5f;
            SetSpellToolTipVar(bonusDamage2, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            damage = GetTotalAttackDamage(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level == 0)
            {
                level = 1;
            }
            spear = this.effect0[level];
            spearDamage = damage * spear;
            SetSpellToolTipVar(spearDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnActivate()
        {
            float aD;
            float bonusDamage2;
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            charVars.CastPoint = 1;
            aD = GetFlatPhysicalDamageMod(owner);
            bonusDamage2 = aD * 0.8f;
            SetSpellToolTipVar(bonusDamage2, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnResurrect()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VayneSilveredBolts(), 1, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}