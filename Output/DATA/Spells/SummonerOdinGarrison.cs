#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerOdinGarrison : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Fortify",
            BuffTextureName = "SummonerGarrison.dds",
        };
        Particle auraParticle;
        Particle particle;
        Particle particle2;
        bool splash;
        public SummonerOdinGarrison(bool splash = default)
        {
            this.splash = splash;
        }
        public override void OnActivate()
        {
            Particle asdf; // UNUSED
            SpellEffectCreate(out this.auraParticle, out _, "Summoner_ally_capture_buf_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle, out _, "Summoner_ally_capture_buf_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle2, out _, "Summoner_capture_Pulse.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SetPhysicalImmune(owner, true);
            SetMagicImmune(owner, true);
            //RequireVar(this.splash);
            ApplyAssistMarker(attacker, owner, 10);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellBuffClear(unit, nameof(Buffs.OdinCaptureChannel));
                SpellEffectCreate(out asdf, out _, "Ezreal_essenceflux_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, false, unit, "root", default, unit, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.auraParticle);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle);
            SetPhysicalImmune(owner, false);
            SetMagicImmune(owner, false);
        }
        public override void OnUpdateStats()
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_NEUTRAL)
            {
                SpellBuffClear(owner, nameof(Buffs.SummonerOdinGarrison));
            }
            else
            {
                SetPhysicalImmune(owner, true);
                SetMagicImmune(owner, true);
                IncFlatPhysicalDamageMod(owner, 0);
                IncPercentCooldownMod(owner, -1);
                IncPAR(owner, 800, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float newDamage;
            if(this.splash)
            {
                newDamage = damageAmount * 0.5f;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(unit != target)
                    {
                        ApplyDamage(attacker, unit, newDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 1, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class SummonerOdinGarrison : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            if(ExecutePeriodically(0.25f, ref avatarVars.LastTimeExecutedGarrison, true))
            {
                avatarVars.CanCastGarrison = false;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUseable | SpellDataFlags.AffectWards, nameof(Buffs.OdinGuardianBuff), true))
                {
                    avatarVars.CanCastGarrison = true;
                }
            }
            returnValue = avatarVars.CanCastGarrison;
            return returnValue;
        }
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown;
            float cooldownMultiplier;
            baseCooldown = 210;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float cooldownMultiplier;
            float baseCooldown;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 210 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void SelfExecute()
        {
            Particle ar; // UNUSED
            bool nextBuffVars_Splash;
            TeamId teamOfOwner;
            TeamId teamOfTarget;
            string slotName;
            SpellEffectCreate(out ar, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 1800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectTurrets | SpellDataFlags.AffectUseable | SpellDataFlags.AffectWards, 1, nameof(Buffs.OdinGuardianBuff), true))
            {
                if(avatarVars.DefensiveMastery == 1)
                {
                    nextBuffVars_Splash = true;
                }
                else
                {
                    nextBuffVars_Splash = false;
                }
                teamOfOwner = GetTeamID(owner);
                teamOfTarget = GetTeamID(unit);
                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinGuardianBuff)) > 0)
                {
                    if(teamOfOwner == teamOfTarget)
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SummonerOdinGarrison(nextBuffVars_Splash), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INVULNERABILITY, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SummonerOdinGarrisonDebuff(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INVULNERABILITY, 0, true, false, false);
                    }
                }
                else
                {
                    slotName = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                    if(slotName == nameof(Spells.SummonerOdinGarrison))
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                    }
                    else
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                    }
                }
            }
        }
    }
}