#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GalioIdolOfDurand : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "GalioIdolOfDurand",
            BuffTextureName = "Galio_IdolOfDurand.dds",
            NonDispellable = true,
        };
        float baseDamage;
        float hitCount;
        Particle areaVFXAlly;
        Particle areaVFXEnemy;
        Particle channelVFX;
        float lastTimeExecuted;
        int[] effect0 = {220, 330, 440};
        public override void OnActivate()
        {
            int level;
            TeamId teamID;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.baseDamage = this.effect0[level];
            this.hitCount = 0;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.areaVFXAlly, out this.areaVFXEnemy, "galio_beguilingStatue_taunt_indicator_team_green.troy", "galio_beguilingStatue_taunt_indicator_team_red.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.channelVFX, out _, "galio_talion_channel.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle explosionVFX; // UNUSED
            float bonusDmgPercent;
            float totalDmgPercent;
            Particle targetVFX; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectRemove(this.channelVFX);
            SpellEffectRemove(this.areaVFXAlly);
            SpellEffectRemove(this.areaVFXEnemy);
            SpellEffectCreate(out explosionVFX, out _, "galio_talion_breakout.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out explosionVFX, out _, "galio_builingStatue_impact_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            this.hitCount = Math.Min(this.hitCount, 8);
            bonusDmgPercent = this.hitCount * 0.05f;
            totalDmgPercent = bonusDmgPercent + 1;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, totalDmgPercent, 0.6f, 1, false, false, (ObjAIBase)owner);
                SpellBuffRemove(unit, nameof(Buffs.Taunt), (ObjAIBase)owner, 0);
                SpellBuffRemove(unit, nameof(Buffs.GalioIdolOfDurandTaunt), (ObjAIBase)owner, 0);
                SpellEffectCreate(out targetVFX, out _, "galio_builingStatue_unit_impact_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, default, default, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.GalioIdolOfDurandMarker), false))
                {
                    ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.GalioIdolOfDurandMarker(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyTaunt(owner, unit, 1.5f);
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.5f;
            }
            if(damageType != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                this.hitCount++;
            }
        }
    }
}
namespace Spells
{
    public class GalioIdolOfDurand : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChannelDuration = 2f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyAssistMarker((ObjAIBase)owner, target, 10);
            AddBuff((ObjAIBase)owner, target, new Buffs.GalioIdolOfDurandMarker(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            BreakSpellShields(target);
            ApplyTaunt(owner, target, 2);
        }
        public override void ChannelingStart()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.GalioIdolOfDurand(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.GalioIdolOfDurand), (ObjAIBase)owner, 0);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage((ObjAIBase)owner, unit, 1, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.GalioIdolOfDurand), (ObjAIBase)owner, 0);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage((ObjAIBase)owner, unit, 1, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
    }
}