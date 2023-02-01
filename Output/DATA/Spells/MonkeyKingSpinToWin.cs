#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingSpinToWin : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "MonkeyKingSpinToWin",
            BuffTextureName = "MonkeyKingCyclone.dds",
        };
        float spellCooldown;
        float baseDamage;
        float moveSpeedMod;
        Particle particle2;
        Particle particleID;
        float lastTimeExecuted;
        public MonkeyKingSpinToWin(float spellCooldown = default, float baseDamage = default, float moveSpeedMod = default)
        {
            this.spellCooldown = spellCooldown;
            this.baseDamage = baseDamage;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            //RequireVar(this.baseDamage);
            //RequireVar(this.moveSpeedMod);
            PlayAnimation("Spell4", 0, owner, true, true, true);
            SpellEffectCreate(out this.particle2, out _, "monkey_king_ult_spin.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particleID, out _, "garen_weapon_glow_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, "BUFFBONE_WEAPON_3", default, false, false, false, false, false);
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonkeyKingDecoyStealth)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDecoyStealth), (ObjAIBase)owner, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            StopCurrentOverrideAnimation("Spell4", owner, true);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particleID);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.MonkeyKingSpinToWin));
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.spellCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            float totalAttackDamage;
            float damagePerSecond;
            float damageToDeal;
            bool isStealthed;
            Particle samPH; // UNUSED
            Particle pH; // UNUSED
            bool canSee;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                teamID = GetTeamID(owner);
                totalAttackDamage = GetTotalAttackDamage(owner);
                totalAttackDamage *= 1.2f;
                damagePerSecond = this.baseDamage + totalAttackDamage;
                damageToDeal = damagePerSecond * 0.5f;
                this.moveSpeedMod += 0.05f;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 315, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    isStealthed = GetStealthed(unit);
                    if(teamID == TeamId.TEAM_BLUE)
                    {
                        if(!isStealthed)
                        {
                            SpellEffectCreate(out samPH, out _, "monkey_king_ult_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerOrder)) == 0)
                            {
                                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerChaos)) == 0)
                                {
                                    AddBuff((ObjAIBase)unit, unit, new Buffs.MonkeyKingSpinMarkerOrder(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                    BreakSpellShields(unit);
                                    SpellEffectCreate(out pH, out _, "monkey_king_ult_unit_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    AddBuff(attacker, unit, new Buffs.MonkeyKingSpinKnockup(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                                }
                            }
                        }
                        else
                        {
                            if(target is Champion)
                            {
                                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                SpellEffectCreate(out samPH, out _, "monkey_king_ult_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerOrder)) == 0)
                                {
                                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerChaos)) == 0)
                                    {
                                        AddBuff((ObjAIBase)unit, unit, new Buffs.MonkeyKingSpinMarkerOrder(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                        BreakSpellShields(unit);
                                        SpellEffectCreate(out pH, out _, "monkey_king_ult_unit_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                        AddBuff(attacker, unit, new Buffs.MonkeyKingSpinKnockup(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                                    }
                                }
                            }
                            else
                            {
                                canSee = CanSeeTarget(owner, target);
                                if(canSee)
                                {
                                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                    SpellEffectCreate(out samPH, out _, "monkey_king_ult_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerOrder)) == 0)
                                    {
                                        if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerChaos)) == 0)
                                        {
                                            AddBuff((ObjAIBase)unit, unit, new Buffs.MonkeyKingSpinMarkerOrder(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                            BreakSpellShields(unit);
                                            SpellEffectCreate(out pH, out _, "monkey_king_ult_unit_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                            AddBuff(attacker, unit, new Buffs.MonkeyKingSpinKnockup(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(!isStealthed)
                        {
                            SpellEffectCreate(out samPH, out _, "monkey_king_ult_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerChaos)) == 0)
                            {
                                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerOrder)) == 0)
                                {
                                    AddBuff((ObjAIBase)unit, unit, new Buffs.MonkeyKingSpinMarkerChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                    BreakSpellShields(unit);
                                    SpellEffectCreate(out pH, out _, "monkey_king_ult_unit_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    AddBuff(attacker, unit, new Buffs.MonkeyKingSpinKnockup(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                                }
                            }
                        }
                        else
                        {
                            if(target is Champion)
                            {
                                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                SpellEffectCreate(out samPH, out _, "monkey_king_ult_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerChaos)) == 0)
                                {
                                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerOrder)) == 0)
                                    {
                                        AddBuff((ObjAIBase)unit, unit, new Buffs.MonkeyKingSpinMarkerChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                        BreakSpellShields(unit);
                                        SpellEffectCreate(out pH, out _, "monkey_king_ult_unit_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                        AddBuff(attacker, unit, new Buffs.MonkeyKingSpinKnockup(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                                    }
                                }
                            }
                            else
                            {
                                canSee = CanSeeTarget(owner, target);
                                if(canSee)
                                {
                                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                    SpellEffectCreate(out samPH, out _, "monkey_king_ult_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerChaos)) == 0)
                                    {
                                        if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonkeyKingSpinMarkerOrder)) == 0)
                                        {
                                            AddBuff((ObjAIBase)unit, unit, new Buffs.MonkeyKingSpinMarkerChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                            BreakSpellShields(unit);
                                            SpellEffectCreate(out pH, out _, "monkey_king_ult_unit_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                            AddBuff(attacker, unit, new Buffs.MonkeyKingSpinKnockup(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class MonkeyKingSpinToWin : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {120, 105, 90, 90, 90};
        int[] effect1 = {20, 110, 200, 0, 0};
        float[] effect2 = {0.015f, 0.015f, 0.015f, 0.015f, 0.015f};
        int[] effect3 = {4, 4, 4};
        public override void SelfExecute()
        {
            float nextBuffVars_SpellCooldown;
            float nextBuffVars_BaseDamage;
            float nextBuffVars_MoveSpeedMod;
            nextBuffVars_SpellCooldown = this.effect0[level];
            nextBuffVars_BaseDamage = this.effect1[level];
            nextBuffVars_MoveSpeedMod = this.effect2[level];
            AddBuff(attacker, owner, new Buffs.MonkeyKingSpinToWin(nextBuffVars_SpellCooldown, nextBuffVars_BaseDamage, nextBuffVars_MoveSpeedMod), 1, 1, this.effect3[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.MonkeyKingSpinToWinLeave));
            SetSlotSpellCooldownTimeVer2(1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}