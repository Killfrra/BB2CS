#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniWintersClaw : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Sejuani_Frost_Arctic.troy", },
            BuffName = "SejuaniFrostArctic",
            BuffTextureName = "Sejuani_Permafrost.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        Particle overhead;
        public SejuaniWintersClaw(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            SpellBuffRemove(owner, nameof(Buffs.SejuaniFrost), caster, 0);
            //RequireVar(this.movementSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out this.overhead, out _, "Sejuani_Frost_Arctic_Overhead.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, attacker, "Bird_head", default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            AttackableUnit caster; // UNITIALIZED
            float duration;
            float nextBuffVars_MovementSpeedMod;
            SpellEffectRemove(this.overhead);
            if(GetBuffCountFromCaster(owner, caster, nameof(Buffs.SejuaniFrostTracker)) > 0)
            {
                duration = GetBuffRemainingDuration(owner, nameof(Buffs.SejuaniFrostTracker));
                SpellBuffRemove(owner, nameof(Buffs.SejuaniFrostTracker), attacker, 0);
                nextBuffVars_MovementSpeedMod = -0.1f;
                AddBuff(attacker, owner, new Buffs.SejuaniFrost(nextBuffVars_MovementSpeedMod), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
        }
    }
}
namespace Spells
{
    public class SejuaniWintersClaw : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = {-0.3f, -0.4f, -0.5f, -0.6f, -0.7f};
        float[] effect1 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        int[] effect2 = {60, 110, 160, 210, 260};
        public override bool CanCast()
        {
            bool returnValue = true;
            TeamId teamID;
            returnValue = false;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrost), true))
                {
                    returnValue = true;
                }
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrostResist), true))
                {
                    returnValue = true;
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrostChaos), true))
                {
                    returnValue = true;
                }
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrostResistChaos), true))
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            TeamId teamID;
            Particle particle1; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out particle1, out _, "Sejuani_WintersClaw_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_UpArm", default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out particle1, out _, "Sejuani_WintersClaw_cas_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            float damageToDeal;
            bool damageThis;
            Particle particle1; // UNUSED
            AttackableUnit unit; // UNITIALIZED
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MovementSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = this.effect1[level];
            damageToDeal = this.effect2[level];
            damageThis = false;
            if(teamID == TeamId.TEAM_BLUE)
            {
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrost)) > 0)
                {
                    damageThis = true;
                }
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrostResist)) > 0)
                {
                    damageThis = true;
                }
                if(damageThis)
                {
                    SpellEffectCreate(out particle1, out _, "Sejuani_WintersClaw_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, unit, default, default, false, false, false, false, false);
                    BreakSpellShields(target);
                    AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniWintersClaw(nextBuffVars_MovementSpeedMod), 1, 1, charVars.FrostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                }
            }
            else
            {
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrostChaos)) > 0)
                {
                    damageThis = true;
                }
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrostResistChaos)) > 0)
                {
                    damageThis = true;
                }
                if(damageThis)
                {
                    SpellEffectCreate(out particle1, out _, "Sejuani_WintersClaw_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, unit, default, default, false, false, false, false, false);
                    BreakSpellShields(target);
                    AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniWintersClawChaos(nextBuffVars_MovementSpeedMod), 1, 1, charVars.FrostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                }
            }
        }
    }
}