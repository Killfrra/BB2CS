#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniNorthernWinds : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "SejuaniNorthernWinds",
            BuffTextureName = "Sejuani_NorthernWinds.dds",
        };
        float damagePerTick;
        float maxHPPercent;
        float frostBonus;
        Particle b;
        float lastTimeExecuted;
        public SejuaniNorthernWinds(float damagePerTick = default, float maxHPPercent = default, float frostBonus = default)
        {
            this.damagePerTick = damagePerTick;
            this.maxHPPercent = maxHPPercent;
            this.frostBonus = frostBonus;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            //RequireVar(this.damagePerTick);
            //RequireVar(this.maxHPPercent);
            //RequireVar(this.frostBonus);
            SpellEffectCreate(out this.b, out _, "Sejuani_NorthernWinds_aura.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.b);
        }
        public override void OnUpdateActions()
        {
            float temp1;
            float percentDamage;
            float damagePerTick;
            float abilityPowerMod;
            float abilityPowerBonus;
            float damagePerTickFrost;
            TeamId teamID;
            bool bonus;
            Particle a; // UNUSED
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                temp1 = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                percentDamage = temp1 * this.maxHPPercent;
                damagePerTick = percentDamage + this.damagePerTick;
                abilityPowerMod = GetFlatMagicDamageMod(owner);
                abilityPowerBonus = abilityPowerMod * 0.1f;
                damagePerTick += abilityPowerBonus;
                damagePerTickFrost = this.frostBonus * damagePerTick;
                teamID = GetTeamID(owner);
                if(teamID == TeamId.TEAM_BLUE)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        bonus = false;
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrost)) > 0)
                        {
                            bonus = true;
                        }
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResist)) > 0)
                        {
                            bonus = true;
                        }
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniWintersClaw)) > 0)
                        {
                            bonus = true;
                        }
                        if(bonus)
                        {
                            ApplyDamage(attacker, unit, damagePerTickFrost, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out a, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                        else
                        {
                            ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out a, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                    }
                }
                else
                {
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        bonus = false;
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostChaos)) > 0)
                        {
                            bonus = true;
                        }
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResistChaos)) > 0)
                        {
                            bonus = true;
                        }
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniWintersClawChaos)) > 0)
                        {
                            bonus = true;
                        }
                        if(bonus)
                        {
                            ApplyDamage(attacker, unit, damagePerTickFrost, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out a, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                        else
                        {
                            ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out a, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class SejuaniNorthernWinds : BBSpellScript
    {
        int[] effect0 = {12, 20, 28, 36, 44};
        float[] effect1 = {0.01f, 0.0125f, 0.015f, 0.0175f, 0.02f};
        public override void SelfExecute()
        {
            float nextBuffVars_DamagePerTick;
            float nextBuffVars_MaxHPPercent;
            float nextBuffVars_FrostBonus;
            nextBuffVars_DamagePerTick = this.effect0[level];
            nextBuffVars_MaxHPPercent = this.effect1[level];
            nextBuffVars_FrostBonus = 1.5f;
            AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniNorthernWinds(nextBuffVars_DamagePerTick, nextBuffVars_MaxHPPercent, nextBuffVars_FrostBonus), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}