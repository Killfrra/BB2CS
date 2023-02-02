#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class DrainChannel : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Particle particleID;
        float drainExecuted;
        Particle glow;
        Particle confetti;
        float[] effect0 = {0.6f, 0.65f, 0.7f, 0.75f, 0.8f};
        int[] effect1 = {30, 45, 60, 75, 90};
        float[] effect2 = {0.6f, 0.65f, 0.7f, 0.75f, 0.8f};
        int[] effect3 = {30, 45, 60, 75, 90};
        public override void ChannelingStart()
        {
            float abilityPower;
            float nextBuffVars_DrainPercent;
            float baseDamage;
            float bonusDamage;
            float damageToDeal;
            TeamId teamID; // UNUSED
            int fiddlesticksSkinID;
            bool nextBuffVars_DrainedBool;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            abilityPower = GetFlatMagicDamageMod(owner);
            AddBuff((ObjAIBase)owner, target, new Buffs.DrainChannel(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Fearmonger_marker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.HEAL, 0, true, false, false);
            SpellEffectCreate(out this.particleID, out _, "Drain.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, target, "spine", default, false, false, false, false, false);
            this.drainExecuted = GetTime();
            nextBuffVars_DrainPercent = this.effect0[level];
            nextBuffVars_DrainedBool = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            baseDamage = this.effect1[level];
            bonusDamage = abilityPower * 0.225f;
            damageToDeal = bonusDamage + baseDamage;
            ApplyDamage((ObjAIBase)owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, (ObjAIBase)owner);
            teamID = GetTeamID(owner);
            fiddlesticksSkinID = GetSkinID(owner);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectCreate(out this.glow, out _, "Party_DrainGlow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, "spine", default, false, false, false, false, false);
                SpellEffectCreate(out this.confetti, out _, "Party_HornConfetti.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "BUFFBONE_CSTM_HORN", default, attacker, default, default, false, false, false, false, false);
            }
        }
        public override void ChannelingUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.drainExecuted, false))
            {
                float distance;
                distance = DistanceBetweenObjects("Target", "Owner");
                if(distance >= 650)
                {
                    StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                }
                if(target.IsDead)
                {
                    StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                }
                else
                {
                    if(owner.IsDead)
                    {
                        SpellEffectRemove(this.particleID);
                    }
                    else
                    {
                        float nextBuffVars_DrainPercent;
                        float abilityPower;
                        float baseDamage;
                        float bonusDamage;
                        float damageToDeal;
                        bool nextBuffVars_DrainedBool;
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        nextBuffVars_DrainPercent = this.effect2[level];
                        nextBuffVars_DrainedBool = false;
                        AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        abilityPower = GetFlatMagicDamageMod(owner);
                        baseDamage = this.effect3[level];
                        bonusDamage = abilityPower * 0.225f;
                        damageToDeal = bonusDamage + baseDamage;
                        ApplyDamage((ObjAIBase)owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
                    }
                }
            }
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID; // UNUSED
            int fiddlesticksSkinID;
            if(target is ObjAIBase)
            {
            }
            else
            {
                SpellBuffRemove(target, nameof(Buffs.Drain), (ObjAIBase)owner, 0);
            }
            SpellBuffRemove(owner, nameof(Buffs.Fearmonger_marker), (ObjAIBase)owner, 0);
            SpellEffectRemove(this.particleID);
            teamID = GetTeamID(owner);
            fiddlesticksSkinID = GetSkinID(owner);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(this.glow);
                SpellEffectRemove(this.confetti);
            }
        }
        public override void ChannelingCancelStop()
        {
            TeamId teamID; // UNUSED
            int fiddlesticksSkinID;
            if(target is ObjAIBase)
            {
            }
            else
            {
                SpellBuffRemove(target, nameof(Buffs.Drain), (ObjAIBase)owner, 0);
            }
            SpellBuffRemove(owner, nameof(Buffs.Fearmonger_marker), (ObjAIBase)owner, 0);
            SpellEffectRemove(this.particleID);
            teamID = GetTeamID(owner);
            fiddlesticksSkinID = GetSkinID(owner);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(this.glow);
                SpellEffectRemove(this.confetti);
            }
        }
    }
}
namespace Buffs
{
    public class DrainChannel : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Drain",
            BuffTextureName = "Fiddlesticks_ConjureScarecrow.dds",
            IsDeathRecapSource = true,
            SpellFXOverrideSkins = new[]{ "SurprisePartyFiddlesticks", },
        };
    }
}