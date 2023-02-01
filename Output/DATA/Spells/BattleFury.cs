#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BattleFury : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Battle Fury",
            BuffTextureName = "DarkChampion_Fury.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusCrit;
        float furyPerHit;
        float furyPerCrit;
        float furyPerKill;
        float lastTimeExecuted2;
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            this.bonusCrit = 0;
            this.furyPerHit = 5;
            this.furyPerCrit = 10;
            this.furyPerKill = 10;
        }
        public override void OnUpdateStats()
        {
            float fury;
            fury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            this.bonusCrit = 0.0035f * fury;
            IncFlatCritChanceMod(owner, this.bonusCrit);
            if(ExecutePeriodically(1, ref this.lastTimeExecuted2, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonInCombat)) == 0)
                {
                    IncPAR(owner, -5, PrimaryAbilityResourceType.Other);
                }
            }
            if(fury >= 3)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BloodlustParticle)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.BloodlustParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                }
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.BloodlustParticle), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            float totalAD;
            float baseDamage;
            float bonusDamage;
            float critDisplay;
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, true))
            {
                totalAD = GetTotalAttackDamage(owner);
                baseDamage = GetBaseAttackDamage(owner);
                bonusDamage = totalAD - baseDamage;
                bonusDamage *= 1.2f;
                critDisplay = 100 * this.bonusCrit;
                SetBuffToolTipVar(1, critDisplay);
                SetSpellToolTipVar(bonusDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnKill()
        {
            int level;
            float cooldown;
            if(target is not BaseTurret)
            {
                if(target is ObjAIBase)
                {
                    IncPAR(owner, this.furyPerKill, PrimaryAbilityResourceType.Other);
                }
            }
            if(1 == 0)
            {
                if(target is Champion)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level >= 1)
                    {
                        cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(cooldown > 0)
                        {
                            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                        }
                    }
                }
            }
        }
        public override void OnAssist()
        {
            int level;
            float cooldown;
            if(1 == 0)
            {
                if(target is Champion)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level >= 1)
                    {
                        cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(cooldown > 0)
                        {
                            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                        }
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is not BaseTurret)
            {
                if(target is ObjAIBase)
                {
                    if(hitResult == HitResult.HIT_Critical)
                    {
                        IncPAR(owner, this.furyPerCrit, PrimaryAbilityResourceType.Other);
                    }
                    else
                    {
                        IncPAR(owner, this.furyPerHit, PrimaryAbilityResourceType.Other);
                    }
                }
            }
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}