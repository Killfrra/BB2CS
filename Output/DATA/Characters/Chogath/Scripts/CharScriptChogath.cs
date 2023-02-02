#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptChogath : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {0, 0, 0};
        int[] effect1 = {300, 475, 650};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                int count;
                count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    float cooldown;
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown <= 0)
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                        {
                            float healthPerStack;
                            float feastBase;
                            float bonusFeastHealth;
                            float feastHealth;
                            float targetHealth;
                            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
                            healthPerStack = this.effect0[level];
                            feastBase = this.effect1[level];
                            bonusFeastHealth = healthPerStack * count;
                            feastHealth = bonusFeastHealth + feastBase;
                            targetHealth = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                            if(feastHealth >= targetHealth)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.FeastMarker(), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VorpalSpikes)) > 0)
            {
                Vector3 castPosition;
                castPosition = GetPointByUnitFacingOffset(owner, 550, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellCast((ObjAIBase)owner, target, castPosition, default, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
        public override void OnMiss()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VorpalSpikes)) > 0)
            {
                Vector3 castPosition;
                castPosition = GetPointByUnitFacingOffset(owner, 550, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellCast((ObjAIBase)owner, target, castPosition, default, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Carnivore(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 2)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VorpalSpikes(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnResurrect()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            if(count == 1)
            {
                OverrideAnimation("Run", "Run1", owner);
            }
            else if(count == 2)
            {
                OverrideAnimation("Run", "Run2", owner);
            }
            else if(count == 3)
            {
                OverrideAnimation("Run", "Run3", owner);
            }
            else if(count == 4)
            {
                OverrideAnimation("Run", "Run4", owner);
            }
            else if(count == 5)
            {
                OverrideAnimation("Run", "Run5", owner);
            }
            else if(count == 6)
            {
                OverrideAnimation("Run", "Run6", owner);
            }
        }
    }
}