#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerPromoteSR : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", "", "", },
            AutoBuffActivateEffect = new[]{ "bloodboil_buf.troy", "bloodboil_buf.troy", },
            BuffName = "SummonerPromoteSR",
            BuffTextureName = "Summoner_PromoteSR.dds",
        };
        float totalMR;
        float bonusArmor;
        float bonusHealth;
        float lastTimeExecuted;
        public SummonerPromoteSR(float totalMR = default, float bonusArmor = default, float bonusHealth = default)
        {
            this.totalMR = totalMR;
            this.bonusArmor = bonusArmor;
            this.bonusHealth = bonusHealth;
        }
        public override void OnActivate()
        {
            Particle aras; // UNUSED
            Particle castParticle; // UNUSED
            TeamId ownerTeamID; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0)
            {
                //RequireVar(this.totalMR);
                //RequireVar(this.bonusArmor);
                RedirectGold(owner, attacker);
                SpellEffectCreate(out aras, out _, "Summoner_Flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                //RequireVar(this.bonusHealth);
                IncPermanentPercentAttackSpeedMod(owner, 0.5f);
                IncPermanentFlatAttackRangeMod(owner, 75);
                IncPermanentFlatSpellBlockMod(owner, this.totalMR);
                IncPermanentFlatArmorMod(owner, this.bonusArmor);
                IncScaleSkinCoef(0.4f, owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinSuperMinion)) > 0)
            {
                RedirectGold(owner, attacker);
                SpellEffectCreate(out aras, out _, "Summoner_Flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                //RequireVar(this.bonusHealth);
                ownerTeamID = GetTeamID(attacker);
                IncPermanentPercentAttackSpeedMod(owner, 0.8f);
                IncPermanentFlatAttackRangeMod(owner, 75);
                IncPermanentPercentCooldownMod(owner, -1);
                IncPermanentFlatSpellBlockMod(owner, -10);
                IncScaleSkinCoef(0.7f, owner);
            }
        }
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0)
            {
                IncFlatHPPoolMod(owner, this.bonusHealth);
                IncScaleSkinCoef(0.4f, owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinSuperMinion)) > 0)
            {
                IncFlatHPPoolMod(owner, this.bonusHealth);
                IncScaleSkinCoef(0.7f, owner);
            }
        }
        public override void OnUpdateActions()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0)
            {
                if(ExecutePeriodically(5, ref this.lastTimeExecuted, false))
                {
                    foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectTurrets, 1, nameof(Buffs.Taunt), false))
                    {
                        ApplyTaunt(unit, owner, 10);
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            Particle gemhit; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0)
            {
                SpellEffectCreate(out gemhit, out _, "GemKnightBasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            }
        }
    }
}
namespace Spells
{
    public class SummonerPromoteSR : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
            {
                if(ExecutePeriodically(0.25f, ref avatarVars.LastTimeExecutedPromote, true))
                {
                    avatarVars.CanCastPromote = false;
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, nameof(Buffs.OdinSuperMinion), true))
                    {
                        if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                        {
                            if(GetBuffCountFromCaster(unit, default, nameof(Buffs.OdinSuperMinion)) > 0)
                            {
                                avatarVars.CanCastPromote = true;
                            }
                        }
                    }
                }
            }
            returnValue = avatarVars.CanCastPromote;
            else
            {
                if(ExecutePeriodically(0.25f, ref avatarVars.LastTimeExecutedPromote, true))
                {
                    avatarVars.CanCastPromote = false;
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, nameof(Buffs.PromoteMeBuff), true))
                    {
                        if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerPromoteSR)) == 0)
                        {
                            if(GetBuffCountFromCaster(unit, default, nameof(Buffs.PromoteMeBuff)) > 0)
                            {
                                avatarVars.CanCastPromote = true;
                            }
                        }
                    }
                }
            }
            returnValue = avatarVars.CanCastPromote;
            return returnValue;
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float baseCooldown;
            float cooldownMultiplier;
            baseCooldown = 180;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            if(avatarVars.PromoteCooldownBonus != 0)
            {
                baseCooldown -= avatarVars.PromoteCooldownBonus;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void SelfExecute()
        {
            int ownerLevel;
            float bonusHealth;
            float nextBuffVars_BonusHealth;
            float nextBuffVars_BonusArmor;
            float nextBuffVars_TotalMR;
            float count;
            string skinName;
            Particle castParticle; // UNUSED
            float bonusArmor;
            float totalMR;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
            {
                ownerLevel = GetLevel(owner);
                bonusHealth = ownerLevel * 75;
                bonusHealth += 200;
                nextBuffVars_BonusHealth = bonusHealth;
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 1, nameof(Buffs.OdinSuperMinion), true))
                {
                    if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) > 0)
                    {
                        foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 2, nameof(Buffs.OdinSuperMinion), true))
                        {
                            if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) > 0)
                            {
                                count++;
                                if(count >= 2)
                                {
                                    foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 3, nameof(Buffs.OdinSuperMinion), true))
                                    {
                                        if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) > 0)
                                        {
                                            count = Math.Max(count, 0);
                                            count++;
                                            if(count >= 3)
                                            {
                                                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 4, nameof(Buffs.OdinSuperMinion), true))
                                                {
                                                    if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                                                    {
                                                        skinName = GetUnitSkinName(unit);
                                                        if(skinName == "OdinBlueSuperminion")
                                                        {
                                                            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                                                            AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                            IncHealth(unit, 10000, unit);
                                                        }
                                                        else if(skinName == "OdinRedSuperminion")
                                                        {
                                                            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                                                            AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                            IncHealth(unit, 10000, unit);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                                        {
                                            skinName = GetUnitSkinName(unit);
                                            if(skinName == "OdinBlueSuperminion")
                                            {
                                                SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                                                AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                IncHealth(unit, 10000, unit);
                                            }
                                            else if(skinName == "OdinRedSuperminion")
                                            {
                                                SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                                                AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                IncHealth(unit, 10000, unit);
                                            }
                                        }
                                    }
                                }
                            }
                            if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                            {
                                skinName = GetUnitSkinName(unit);
                                if(skinName == "OdinBlueSuperminion")
                                {
                                    SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                                    AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                    IncHealth(unit, 10000, unit);
                                }
                                else if(skinName == "OdinRedSuperminion")
                                {
                                    SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                                    AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                    IncHealth(unit, 10000, unit);
                                }
                            }
                        }
                    }
                    if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                    {
                        skinName = GetUnitSkinName(unit);
                        if(skinName == "OdinBlueSuperminion")
                        {
                            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                            AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                            IncHealth(unit, 10000, unit);
                        }
                        else if(skinName == "OdinRedSuperminion")
                        {
                            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, false, false, false, false);
                            AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                            IncHealth(unit, 10000, unit);
                        }
                    }
                }
            }
            else
            {
                ownerLevel = GetLevel(owner);
                bonusHealth = ownerLevel * 100;
                bonusHealth += 100;
                nextBuffVars_BonusHealth = bonusHealth;
                bonusArmor = 5 * ownerLevel;
                bonusArmor += 20;
                nextBuffVars_BonusArmor = bonusArmor;
                totalMR = 0.75f * ownerLevel;
                totalMR += 10;
                nextBuffVars_TotalMR = totalMR;
                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, attacker.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AlwaysSelf, 1, nameof(Buffs.PromoteMeBuff), true))
                {
                    AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SpellBuffRemove(unit, nameof(Buffs.PromoteBuff), (ObjAIBase)owner, 0);
                }
            }
        }
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown;
            float cooldownMultiplier;
            baseCooldown = 180;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
    }
}