#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GlacialStorm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            SpellToggleSlot = 4,
        };
        float damagePerLevel;
        float manaCost;
        Vector3 targetPos;
        Particle particle;
        Particle particle2;
        float damageManaTimer;
        float slowTimer;
        int[] effect0 = {6, 6, 6};
        public GlacialStorm(float damagePerLevel = default, float manaCost = default, Vector3 targetPos = default)
        {
            this.damagePerLevel = damagePerLevel;
            this.manaCost = manaCost;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            float nextBuffVars_AttackSpeedMod;
            float nextBuffVars_MovementSpeedMod;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 1);
            //RequireVar(this.damagePerLevel);
            //RequireVar(this.manaCost);
            //RequireVar(this.targetPos);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
            targetPos = this.targetPos;
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out this.particle2, "cryo_storm_green_team.troy", "cryo_storm_red_team.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, this.damagePerLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.125f, 1, false, false, attacker);
                nextBuffVars_AttackSpeedMod = -0.2f;
                nextBuffVars_MovementSpeedMod = -0.2f;
                AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            int level;
            float baseCooldown;
            float multiplier;
            float newCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseCooldown = this.effect0[level];
            multiplier = 1 + cooldownStat;
            newCooldown = baseCooldown * multiplier;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Area, owner);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            float curMana;
            Vector3 targetPos;
            float negMana;
            float nextBuffVars_AttackSpeedMod;
            float nextBuffVars_MovementSpeedMod;
            bool canCast;
            int level; // UNUSED
            Vector3 ownerPos;
            float distance;
            if(ExecutePeriodically(0.5f, ref this.damageManaTimer, false))
            {
                curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                targetPos = this.targetPos;
                if(this.manaCost > curMana)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    negMana = this.manaCost * -1;
                    IncPAR(owner, negMana, PrimaryAbilityResourceType.MANA);
                }
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, this.damagePerLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.125f, 1, false, false, attacker);
                    nextBuffVars_AttackSpeedMod = -0.2f;
                    nextBuffVars_MovementSpeedMod = -0.2f;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            if(ExecutePeriodically(0.25f, ref this.slowTimer, false))
            {
                canCast = GetCanCast(owner);
                if(!canCast)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                targetPos = this.targetPos;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ownerPos = GetUnitPosition(owner);
                distance = DistanceBetweenPoints(ownerPos, targetPos);
                if(distance >= 1200)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    nextBuffVars_AttackSpeedMod = -0.2f;
                    nextBuffVars_MovementSpeedMod = -0.2f;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class GlacialStorm : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {40, 60, 80};
        int[] effect1 = {20, 25, 30};
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.GlacialStorm)) > 0)
            {
            }
            else
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos;
            float nextBuffVars_DamagePerLevel;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_ManaCost;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.GlacialStorm)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.GlacialStorm), (ObjAIBase)owner, 0);
            }
            else
            {
                targetPos = GetCastSpellTargetPos();
                nextBuffVars_DamagePerLevel = this.effect0[level];
                nextBuffVars_TargetPos = targetPos;
                nextBuffVars_ManaCost = this.effect1[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.GlacialStorm(nextBuffVars_DamagePerLevel, nextBuffVars_ManaCost, nextBuffVars_TargetPos), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}