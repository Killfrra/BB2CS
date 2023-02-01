#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InfectedCleaverMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "InfectedCleaverDebuff",
            BuffTextureName = "DrMundo_InfectedCleaver.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
            SpellFXOverrideSkins = new[]{ "MundoMundo", },
        };
        float moveMod;
        Particle slow;
        public InfectedCleaverMissile(float moveMod = default)
        {
            this.moveMod = moveMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveMod);
            SpellEffectCreate(out this.slow, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.slow);
        }
        public override void OnUpdateStats()
        {
            float moveMod;
            moveMod = this.moveMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, moveMod);
        }
    }
}
namespace Spells
{
    public class InfectedCleaverMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {80, 130, 180, 230, 280};
        float[] effect1 = {-0.4f, -0.4f, -0.4f, -0.4f, -0.4f};
        float[] effect2 = {0.15f, 0.18f, 0.21f, 0.23f, 0.25f};
        int[] effect3 = {300, 400, 500, 600, 700};
        int[] effect4 = {25, 30, 35, 40, 45};
        int[] effect5 = {80, 130, 180, 230, 280};
        float[] effect6 = {-0.4f, -0.4f, -0.4f, -0.4f, -0.4f};
        float[] effect7 = {0.15f, 0.18f, 0.21f, 0.23f, 0.25f};
        int[] effect8 = {300, 400, 500, 600, 700};
        int[] effect9 = {25, 30, 35, 40, 45};
        int[] effect10 = {80, 130, 180, 230, 280};
        float[] effect11 = {-0.4f, -0.4f, -0.4f, -0.4f, -0.4f};
        float[] effect12 = {0.15f, 0.18f, 0.21f, 0.23f, 0.25f};
        int[] effect13 = {300, 400, 500, 600, 700};
        int[] effect14 = {25, 30, 35, 40, 45};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            int mundoID;
            bool isStealthed;
            float minDamage;
            float nextBuffVars_MoveMod;
            float damageMod;
            float maxDamage;
            float health;
            float percentDamage;
            float tempDamage;
            float damageDealt;
            float healthReturn;
            Particle hit; // UNUSED
            bool canSee;
            teamID = GetTeamID(owner);
            mundoID = GetSkinID(owner);
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                minDamage = this.effect0[level];
                nextBuffVars_MoveMod = this.effect1[level];
                damageMod = this.effect2[level];
                maxDamage = this.effect3[level];
                health = GetHealth(target, PrimaryAbilityResourceType.MANA);
                percentDamage = health * damageMod;
                tempDamage = Math.Max(minDamage, percentDamage);
                damageDealt = Math.Min(tempDamage, maxDamage);
                ApplyDamage(attacker, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                AddBuff(attacker, target, new Buffs.InfectedCleaverMissile(nextBuffVars_MoveMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                DestroyMissile(missileNetworkID);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                healthReturn = this.effect4[level];
                IncHealth(owner, healthReturn, owner);
                if(mundoID == 4)
                {
                    SpellEffectCreate(out hit, out _, "dr_mundo_as_mundo_infected_cleaver_tar", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out hit, out _, "dr_mundo_infected_cleaver_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                }
            }
            else
            {
                if(target is Champion)
                {
                    minDamage = this.effect5[level];
                    nextBuffVars_MoveMod = this.effect6[level];
                    damageMod = this.effect7[level];
                    maxDamage = this.effect8[level];
                    health = GetHealth(target, PrimaryAbilityResourceType.MANA);
                    percentDamage = health * damageMod;
                    tempDamage = Math.Max(minDamage, percentDamage);
                    damageDealt = Math.Min(tempDamage, maxDamage);
                    ApplyDamage(attacker, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.InfectedCleaverMissile(nextBuffVars_MoveMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    DestroyMissile(missileNetworkID);
                    if(mundoID == 4)
                    {
                        SpellEffectCreate(out hit, out _, "dr_mundo_as_mundo_infected_cleaver_tar", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    }
                    else
                    {
                        SpellEffectCreate(out hit, out _, "dr_mundo_infected_cleaver_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    }
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    healthReturn = this.effect9[level];
                    IncHealth(owner, healthReturn, owner);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        minDamage = this.effect10[level];
                        nextBuffVars_MoveMod = this.effect11[level];
                        damageMod = this.effect12[level];
                        maxDamage = this.effect13[level];
                        health = GetHealth(target, PrimaryAbilityResourceType.MANA);
                        percentDamage = health * damageMod;
                        tempDamage = Math.Max(minDamage, percentDamage);
                        damageDealt = Math.Min(tempDamage, maxDamage);
                        ApplyDamage(attacker, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.InfectedCleaverMissile(nextBuffVars_MoveMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        DestroyMissile(missileNetworkID);
                        if(mundoID == 4)
                        {
                            SpellEffectCreate(out hit, out _, "dr_mundo_as_mundo_infected_cleaver_tar", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        }
                        else
                        {
                            SpellEffectCreate(out hit, out _, "dr_mundo_infected_cleaver_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        }
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        healthReturn = this.effect14[level];
                        IncHealth(owner, healthReturn, owner);
                    }
                }
            }
        }
    }
}