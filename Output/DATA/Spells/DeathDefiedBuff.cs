﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeathDefiedBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Death Defied Buff",
            BuffTextureName = "Lich_Defied.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        bool cOTGFound; // UNUSED
        Particle particle;
        float cost0;
        float cost2;
        float cost3;
        int[] effect0 = {-20, -26, -32, -38, -44};
        int[] effect1 = {-30, -42, -54, -66, -78};
        int[] effect2 = {-150, -175, -200};
        int[] effect3 = {20, 26, 32, 38, 44};
        int[] effect4 = {30, 42, 54, 66, 78};
        int[] effect5 = {150, 175, 200};
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type != BuffType.INTERNAL)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            float itemCD1; // UNUSED
            int level;
            float cost0;
            this.cOTGFound = false;
            itemCD1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            SpellEffectCreate(out this.particle, out _, "mordekeiser_cotg_skin.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(0, SpellSlotType.InventorySlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.InventorySlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.InventorySlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.InventorySlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(4, SpellSlotType.InventorySlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(5, SpellSlotType.InventorySlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            ShowHealthBar(owner, false);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.SHRED);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.INVISIBILITY);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level != 0)
            {
                cost0 = this.effect0[level];
                SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, cost0, PrimaryAbilityResourceType.MANA);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level != 0)
            {
                SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, -100, PrimaryAbilityResourceType.MANA);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level != 0)
            {
                cost0 = this.effect1[level];
                SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, cost0, PrimaryAbilityResourceType.MANA);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Defile)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Defile(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level != 0)
            {
                float cost3;
                cost3 = this.effect2[level];
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, cost3, PrimaryAbilityResourceType.MANA);
            }
            IncPAR(owner, 5000, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            ForceDead(owner);
            SetTargetable(owner, true);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(this.particle);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(0, SpellSlotType.InventorySlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.InventorySlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.InventorySlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.InventorySlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(4, SpellSlotType.InventorySlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(5, SpellSlotType.InventorySlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            ShowHealthBar(owner, true);
            SpellBuffRemove(owner, nameof(Buffs.Defile), (ObjAIBase)owner, 0);
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            if(lifeTime >= 3.25f)
            {
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level;
            float costInc;
            if(slot == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.cost0 = this.effect3[level];
                costInc = this.cost0 * -1;
                SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, costInc, PrimaryAbilityResourceType.MANA);
            }
            else if(slot == 2)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.cost2 = this.effect4[level];
                costInc = this.cost2 * -1;
                SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, costInc, PrimaryAbilityResourceType.MANA);
            }
            else if(slot == 3)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.cost3 = this.effect5[level];
                costInc = this.cost3 * -1;
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, costInc, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}