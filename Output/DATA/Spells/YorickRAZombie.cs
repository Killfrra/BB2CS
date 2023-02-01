﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickRAZombie : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "yorick_ult_04.troy", "yorick_ult_revive_tar.troy", "yorick_ult_05.troy", },
            BuffName = "YorickOmenReanimated",
            BuffTextureName = "YorickOmenOfDeath.dds",
            NonDispellable = true,
            OnPreDamagePriority = 3,
            PersistsThroughDeath = true,
        };
        Particle particle3;
        Particle particle4;
        bool hasHealed;
        float totalHealth;
        float totalPAR;
        float totalPAREnergy;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle3, out this.particle4, "yorick_ult_03_teamID_green.troy", "yorick_ult_03_teamID_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            this.hasHealed = false;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
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
            this.totalHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            this.totalPAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            this.totalPAREnergy = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            IncHealth(owner, this.totalHealth, owner);
            this.hasHealed = true;
            IncPAR(owner, this.totalPAR, PrimaryAbilityResourceType.MANA);
            IncPAR(owner, this.totalPAREnergy, PrimaryAbilityResourceType.Energy);
            IncPermanentPercentHPRegenMod(owner, -1);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SpellEffectRemove(this.particle3);
            SpellEffectRemove(this.particle4);
            IncPermanentPercentHPRegenMod(owner, 1);
            AddBuff((ObjAIBase)owner, owner, new Buffs.YorickUltStun(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TechmaturgicalIcon)) > 0)
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 20000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, default, true))
                {
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.H28GEvolutionTurret)) > 0)
                    {
                        ApplyDamage(attacker, unit, 1000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_RAW, 1, 1, 0, false, false, attacker);
                    }
                }
            }
            ForceDead(owner);
        }
        public override void OnUpdateActions()
        {
            float maxHealth;
            float currentHealth;
            float healthDecay;
            if(ExecutePeriodically(0.95f, ref this.lastTimeExecuted, false))
            {
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                healthDecay = 0.1f * maxHealth;
                healthDecay++;
                if(healthDecay >= currentHealth)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UndyingRage)) > 0)
                    {
                    }
                    else
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
                else
                {
                }
            }
        }
        public override void OnPreMitigationDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentHealth;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UndyingRage)) > 0)
            {
            }
            else
            {
                currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if(damageAmount >= currentHealth)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float effectiveHeal;
            if(this.hasHealed)
            {
                if(health >= 0)
                {
                    effectiveHeal = health * 0;
                    returnValue = effectiveHeal;
                }
            }
            return returnValue;
        }
    }
}