#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptNidalee_Cougar : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {0, 0, 0};
        int[] effect1 = {300, 550, 800};
        int[] effect2 = {40, 42, 45, 47, 50, 52, 55, 57, 60, 62, 65, 67, 70, 72, 75, 77, 80, 82};
        public override void OnUpdateActions()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.VorpalSpikes(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            }
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
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes))
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
                                AddBuff((ObjAIBase)owner, unit, new Buffs.FeastMarker(), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
                            }
                        }
                    }
                }
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.HealAmount = this.effect2[level];
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                Vector3 missileEndPosition;
                missileEndPosition = GetPointByUnitFacingOffset(owner, 550, 0);
                SpellCast((ObjAIBase)owner, target, missileEndPosition, default, 0, SpellSlotType.ExtraSlots, level, true, true, false);
            }
        }
        public override void OnKill()
        {
            Particle particle; // UNUSED
            IncHealth(owner, charVars.HealAmount, owner);
            SpellEffectCreate(out particle, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Carnivore(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
        }
        public override void OnDisconnect()
        {
            TeamId teamID;
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false);
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE))
                {
                    if(owner != unit)
                    {
                        IncPermanentFlatGoldPer10Mod(unit, 4);
                        IncPermanentPercentEXPBonus(unit, 0.04f);
                    }
                }
            }
            else
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE))
                {
                    if(owner != unit)
                    {
                        IncPermanentFlatGoldPer10Mod(unit, 4);
                        IncPermanentPercentEXPBonus(unit, 0.04f);
                    }
                }
            }
            SetDisableAmbientGold(owner, true);
        }
        public override void OnReconnect()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE))
                {
                    if(owner != unit)
                    {
                        IncPermanentFlatGoldPer10Mod(unit, -4);
                        IncPermanentPercentEXPBonus(unit, -0.04f);
                    }
                }
            }
            else
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE))
                {
                    if(owner != unit)
                    {
                        IncPermanentFlatGoldPer10Mod(unit, -4);
                        IncPermanentPercentEXPBonus(unit, -0.04f);
                    }
                }
            }
            SetDisableAmbientGold(owner, false);
        }
    }
}