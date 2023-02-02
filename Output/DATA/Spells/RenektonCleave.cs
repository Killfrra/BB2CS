#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RenektonCleave : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {5, 5, 5, 5, 5};
        int[] effect1 = {60, 90, 120, 150, 180};
        int[] effect2 = {150, 225, 300, 375, 450};
        int[] effect3 = {50, 75, 100, 125, 150};
        public override void SelfExecute()
        {
            TeamId teamID;
            float ragePercent;
            float healthPercent;
            float nextBuffVars_DrainPercent;
            float furyGainIncrement;
            float furyGain;
            float bonusDamage;
            float weaponDamage;
            float baseDamage;
            float damageToDeal;
            float rangeVar;
            Particle c; // UNUSED
            bool shouldHit;
            bool visible;
            int nextBuffVars_MaxDrain;
            teamID = GetTeamID(owner);
            ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Other);
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.Other);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimationCleave(), 1, 1, 0.24f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell1", 0, owner, false, false, true);
            furyGainIncrement = this.effect0[level];
            furyGain = 0;
            bonusDamage = this.effect1[level];
            weaponDamage = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            weaponDamage -= baseDamage;
            weaponDamage *= 0.8f;
            damageToDeal = bonusDamage + weaponDamage;
            rangeVar = 400;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonReignOfTheTyrant)) > 0)
            {
                rangeVar *= 1.2f;
            }
            if(ragePercent >= 0.5f)
            {
                SpellEffectCreate(out c, out _, "renektoncleave_trail_rage.troy ", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "spine", default, owner, default, default, true, default, default, false);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, rangeVar, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    shouldHit = true;
                    visible = CanSeeTarget(owner, unit);
                    if(!visible)
                    {
                        if(unit is not Champion)
                        {
                            shouldHit = false;
                        }
                    }
                    if(shouldHit)
                    {
                        nextBuffVars_DrainPercent = 0.1f;
                        nextBuffVars_MaxDrain = this.effect2[level];
                        AddBuff(attacker, target, new Buffs.RenektonCleaveDrain(nextBuffVars_DrainPercent, nextBuffVars_MaxDrain), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        if(visible)
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonBloodSplatterTarget(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        if(unit is Champion)
                        {
                            ApplyDamage((ObjAIBase)owner, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0, 0, true, false, attacker);
                        }
                        else
                        {
                            ApplyDamage((ObjAIBase)owner, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0, 0, true, false, attacker);
                        }
                    }
                }
                IncPAR(owner, -50, PrimaryAbilityResourceType.Other);
                SpellBuffClear(owner, nameof(Buffs.RenektonRageReady));
            }
            else
            {
                SpellEffectCreate(out c, out _, "renektoncleave_trail.troy ", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "spine", default, owner, default, default, true, default, default, false);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, rangeVar, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    shouldHit = true;
                    visible = CanSeeTarget(owner, unit);
                    if(!visible)
                    {
                        if(unit is not Champion)
                        {
                            shouldHit = false;
                        }
                    }
                    if(shouldHit)
                    {
                        nextBuffVars_DrainPercent = 0.05f;
                        nextBuffVars_MaxDrain = this.effect3[level];
                        BreakSpellShields(unit);
                        AddBuff(attacker, target, new Buffs.RenektonCleaveDrain(nextBuffVars_DrainPercent, nextBuffVars_MaxDrain), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if(unit is Champion)
                        {
                            ApplyDamage((ObjAIBase)owner, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, true, false, attacker);
                        }
                        else
                        {
                            ApplyDamage((ObjAIBase)owner, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, true, false, attacker);
                        }
                        furyGain += furyGainIncrement;
                        furyGain = Math.Min(furyGain, 25);
                        if(visible)
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonBloodSplatterTarget(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
                IncPAR(owner, furyGain, PrimaryAbilityResourceType.Other);
                if(healthPercent <= charVars.RageThreshold)
                {
                    furyGain *= 0.5f;
                    IncPAR(owner, furyGain, PrimaryAbilityResourceType.Other);
                }
            }
            SpellBuffClear(owner, nameof(Buffs.RenektonCleaveDrain));
        }
    }
}