#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TimeBomb : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {90, 145, 200, 260, 320};
        int[] effect1 = {90, 145, 200, 260, 320};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageLevel;
            Particle par; // UNUSED
            nextBuffVars_DamageLevel = this.effect0[level];
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.TimeBomb)) > 0)
            {
                SpellEffectCreate(out par, out _, "TimeBombExplo.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, target.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, nextBuffVars_DamageLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, attacker);
                }
            }
            else
            {
                TeamId ownerID;
                Champion caster;
                ownerID = GetTeamID(owner);
                if(ownerID == TeamId.TEAM_BLUE)
                {
                    caster = GetChampionBySkinName("Zilean", TeamId.TEAM_PURPLE);
                }
                else
                {
                    caster = GetChampionBySkinName("Zilean", TeamId.TEAM_BLUE);
                }
                if(GetBuffCountFromCaster(target, caster, nameof(Buffs.TimeBomb)) > 0)
                {
                    float damageToDeal;
                    SpellEffectCreate(out par, out _, "TimeBombExplo.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    damageToDeal = this.effect1[level];
                    foreach(AttackableUnit unit in GetUnitsInArea(caster, target.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        ApplyDamage(caster, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 0, false, false, caster);
                    }
                }
            }
            if(!target.IsDead)
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.TimeBomb(nextBuffVars_DamageLevel), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 1, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class TimeBomb : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Time Bomb",
            BuffTextureName = "Chronokeeper_Chronoblast.dds",
        };
        float damageLevel;
        Particle particleID2;
        Particle particleID;
        float tickDamage;
        public TimeBomb(float damageLevel = default)
        {
            this.damageLevel = damageLevel;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.damageLevel);
            teamOfOwner = GetTeamID(attacker);
            SpellEffectCreate(out this.particleID2, out this.particleID, "TimeBomb_green.troy", "TimeBomb_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 500, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            this.tickDamage = 3;
        }
        public override void OnDeactivate(bool expired)
        {
            Particle par; // UNUSED
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.particleID2);
            if(owner.IsDead)
            {
                TeamId teamID;
                teamID = GetTeamID(attacker);
                SpellEffectCreate(out par, out _, "TimeBombExplo.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 500, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, default, default, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, this.damageLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, attacker);
                }
            }
            else if(expired)
            {
                SpellEffectCreate(out par, out _, "TimeBombExplo.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, this.damageLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, attacker);
                }
            }
        }
        public override void OnUpdateActions()
        {
            if(owner.Team != attacker.Team)
            {
                if(this.tickDamage > 0)
                {
                    float nextBuffVars_TickDamage;
                    nextBuffVars_TickDamage = this.tickDamage;
                    AddBuff(attacker, owner, new Buffs.TimeBombCountdown(nextBuffVars_TickDamage), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, owner, this.tickDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                    this.tickDamage--;
                }
            }
        }
    }
}