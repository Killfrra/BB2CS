#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenBladestorm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "GarenBladestorm",
            BuffTextureName = "Garen_KeepingthePeace.dds",
        };
        Particle particle2;
        Particle particleID;
        float spellCooldown;
        float baseDamage;
        float lastTimeExecuted;
        public GarenBladestorm(float spellCooldown = default, float baseDamage = default)
        {
            this.spellCooldown = spellCooldown;
            this.baseDamage = baseDamage;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            float hardnessPercent;
            float reversalDivisor;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.SLOW)
                {
                    hardnessPercent = GetPercentHardnessMod(owner);
                    if(hardnessPercent > 0.5f)
                    {
                    }
                    else
                    {
                        duration *= 0.5f;
                        reversalDivisor = 1 - hardnessPercent;
                        duration /= reversalDivisor;
                    }
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Spell3", owner);
            SpellEffectCreate(out this.particle2, out _, "garen_bladeStorm_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particleID, out _, "garen_weapon_glow_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, "BUFFBONE_WEAPON_3", default, false, false, false, false, false);
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.baseDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GarenBladestorm));
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            StopCurrentOverrideAnimation("Spell3", owner, false);
            ClearOverrideAnimation("Run", owner);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.spellCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particleID);
        }
        public override void OnUpdateActions()
        {
            float critChance;
            float critDamage;
            int level; // UNUSED
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float ratioDamage;
            float preBonusCrit;
            float damageToDealHero;
            float critHero;
            float critMinion;
            float damageToDeal;
            TeamId teamID;
            Particle bSCritPH; // UNUSED
            Particle samPH; // UNUSED
            bool canSee;
            bool isStealthed;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                critChance = GetFlatCritChanceMod(owner);
                critDamage = GetFlatCritDamageMod(owner);
                critDamage += 2;
                PlayAnimation("Spell3", 0, owner, true, false, true);
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                totalDamage = GetTotalAttackDamage(owner);
                baseDamage = GetBaseAttackDamage(owner);
                bonusDamage = totalDamage - baseDamage;
                ratioDamage = 0.7f * bonusDamage;
                preBonusCrit = ratioDamage * critDamage;
                damageToDealHero = ratioDamage + this.baseDamage;
                critHero = preBonusCrit + this.baseDamage;
                critMinion = critHero / 2;
                damageToDeal = damageToDealHero / 2;
                teamID = GetTeamID(owner);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(unit is Champion)
                    {
                        if(RandomChance() < critChance)
                        {
                            ApplyDamage(attacker, unit, critHero, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            SpellEffectCreate(out bSCritPH, out _, "garen_bladestormCrit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                        else
                        {
                            ApplyDamage(attacker, unit, damageToDealHero, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            SpellEffectCreate(out samPH, out _, "garen_keeper0fPeace_tar_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                    }
                    else
                    {
                        canSee = CanSeeTarget(owner, unit);
                        if(canSee)
                        {
                            if(RandomChance() < critChance)
                            {
                                ApplyDamage(attacker, unit, critMinion, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                SpellEffectCreate(out bSCritPH, out _, "garen_bladestormCrit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            }
                            else
                            {
                                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                SpellEffectCreate(out samPH, out _, "garen_keeper0fPeace_tar_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, false, false, false, false);
                            }
                        }
                        else
                        {
                            isStealthed = GetStealthed(unit);
                            if(!isStealthed)
                            {
                                if(RandomChance() < critChance)
                                {
                                    ApplyDamage(attacker, unit, critMinion, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                    SpellEffectCreate(out bSCritPH, out _, "garen_bladestormCrit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                }
                                else
                                {
                                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                    SpellEffectCreate(out samPH, out _, "garen_keeper0fPeace_tar_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, false, false, false, false);
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
    public class GarenBladestorm : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {25, 45, 65, 85, 105};
        int[] effect1 = {13, 12, 11, 10, 9};
        float[] effect2 = {0.5f, 0.5f, 0.5f, 0.5f, 0.5f};
        public override void SelfExecute()
        {
            float nextBuffVars_baseDamage;
            float nextBuffVars_SpellCooldown;
            float nextBuffVars_MoveSpeedMod;
            nextBuffVars_baseDamage = this.effect0[level];
            nextBuffVars_SpellCooldown = this.effect1[level];
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GarenBladestormLeave));
            SpellBuffRemoveType(owner, BuffType.SLOW);
            AddBuff(attacker, owner, new Buffs.GarenBladestorm(nextBuffVars_SpellCooldown, nextBuffVars_baseDamage), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_MoveSpeedMod = this.effect2[level];
            SetSlotSpellCooldownTimeVer2(1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}