#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AbsoluteZero : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 3f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {-0.5f, -0.5f, -0.5f};
        float[] effect1 = {-0.25f, -0.25f, -0.25f};
        int[] effect2 = {625, 875, 1125};
        int[] effect3 = {625, 875, 1125};
        public override void ChannelingStart()
        {
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_MovementSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.AbsoluteZero(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 10, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID;
            Particle asdf; // UNUSED
            teamID = GetTeamID(attacker);
            SpellBuffRemove(owner, nameof(Buffs.AbsoluteZero), (ObjAIBase)owner, 0);
            SpellEffectCreate(out asdf, out _, "AbsoluteZero_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out asdf, out _, "AbsoluteZero_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 2.5f, 1, false, false, attacker);
            }
        }
        public override void ChannelingCancelStop()
        {
            TeamId teamID;
            float secondDamage;
            float totalTime;
            Particle asdf; // UNUSED
            teamID = GetTeamID(attacker);
            secondDamage = this.effect3[level];
            totalTime = 0.25f * charVars.LifeTime;
            SpellEffectCreate(out asdf, out _, "AbsoluteZero_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out asdf, out _, "AbsoluteZero_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, secondDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, totalTime, 2.5f, 1, false, false, attacker);
            }
            SpellBuffRemove(owner, nameof(Buffs.AbsoluteZero), (ObjAIBase)owner, 0);
        }
    }
}
namespace Buffs
{
    public class AbsoluteZero : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "AbsoluteZero3_cas.troy", },
            BuffName = "Absolute Zero",
            BuffTextureName = "Yeti_Shatter.dds",
            SpellFXOverrideSkins = new[]{ "NunuBot", },
            SpellVOOverrideSkins = new[]{ "", },
        };
        Particle particle;
        Particle particle2;
        float movementSpeedMod;
        float attackSpeedMod;
        float lastTimeExecuted;
        public AbsoluteZero(float movementSpeedMod = default, float attackSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out this.particle2, "AbsoluteZero2_green_cas.troy", "AbsoluteZero2_red_cas.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_PURPLE, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            //RequireVar(this.movementSpeedMod);
            //RequireVar(this.attackSpeedMod);
            nextBuffVars_MovementSpeedMod = this.movementSpeedMod;
            nextBuffVars_AttackSpeedMod = this.attackSpeedMod;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.AbsoluteZeroSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            charVars.LifeTime = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MovementSpeedMod;
                    float nextBuffVars_AttackSpeedMod;
                    nextBuffVars_MovementSpeedMod = this.movementSpeedMod;
                    nextBuffVars_AttackSpeedMod = this.attackSpeedMod;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.AbsoluteZeroSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            charVars.LifeTime = lifeTime;
        }
    }
}