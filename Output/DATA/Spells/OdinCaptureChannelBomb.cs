#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OdinCaptureChannelBomb : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 4.5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle particleID;
        Particle particleID2;
        int chargeTimePassed;
        public override void ChannelingStart()
        {
            int count; // UNUSED
            TeamId teamOfOwner; // UNUSED
            SpellEffectCreate(out this.particleID, out _, "OdinCaptureBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out this.particleID2, out _, "OdinCaptureBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
            count = GetBuffCountFromAll(target, nameof(Buffs.OdinBombSuppression));
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureChannelBomb(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, true);
            teamOfOwner = GetTeamID(owner);
            this.chargeTimePassed = 0;
            AddBuff(attacker, attacker, new Buffs.OdinChannelVision(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void ChannelingUpdateStats()
        {
            if(this.chargeTimePassed == 0)
            {
                float accumTime; // UNITIALIZED
                if(accumTime > 1.5f)
                {
                    this.chargeTimePassed = 1;
                    SpellEffectRemove(this.particleID);
                    SpellEffectRemove(this.particleID2);
                    SpellEffectCreate(out this.particleID, out _, "OdinCaptureBeamEngaged.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
                    SpellEffectCreate(out this.particleID2, out _, "OdinCaptureBeamEngaged.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
                    UnlockAnimation(owner, true);
                    PlayAnimation("Channel", 0, owner, true, true, false);
                }
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannelBomb)) == 0)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
            }
            if(this.chargeTimePassed == 1)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannelBomb)) > 0)
                {
                    int count;
                    count = GetBuffCountFromAll(target, nameof(Buffs.OdinBombSuppression));
                    if(count == 0)
                    {
                        TeamId teamOfOwner;
                        teamOfOwner = GetTeamID(owner);
                        if(teamOfOwner == TeamId.TEAM_BLUE)
                        {
                            AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombSuppressionOrder(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombSuppressionChaos(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        }
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombSuppression(), 1, 1, 10, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void ChannelingSuccessStop()
        {
            int cooldownToSet; // UNUSED
            SpellEffectRemove(this.particleID);
            SpellBuffRemove(target, nameof(Buffs.OdinGuardianSuppressionBomb), attacker, 0);
            SpellEffectRemove(this.particleID2);
            SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
            if(target.IsDead)
            {
                cooldownToSet = 0;
                SetUseSlotSpellCooldownTime(0, owner, false);
            }
            else
            {
                cooldownToSet = 4;
                SetUseSlotSpellCooldownTime(4, owner, false);
            }
        }
        public override void ChannelingCancelStop()
        {
            int cooldownToSet; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinBombSuccessParticle)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureChannelBomb), (ObjAIBase)owner, 0);
                SpellEffectRemove(this.particleID);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionChaos), 1);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionOrder), 1);
                SpellEffectRemove(this.particleID2);
                SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
                if(target.IsDead)
                {
                    cooldownToSet = 0;
                    SetUseSlotSpellCooldownTime(0, owner, false);
                }
                else
                {
                    cooldownToSet = 4;
                    SetUseSlotSpellCooldownTime(4, owner, false);
                }
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppression), 1);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureChannelBomb), (ObjAIBase)owner, 0);
                SpellEffectCreate(out _, out _, "OdinCaptureCancel.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, "spine", default, false, false, false, false, false);
                SpellEffectRemove(this.particleID);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionChaos), 1);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionOrder), 1);
                SpellEffectRemove(this.particleID2);
                SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
                if(target.IsDead)
                {
                    cooldownToSet = 0;
                    SetUseSlotSpellCooldownTime(0, owner, false);
                }
                else
                {
                    cooldownToSet = 4;
                    SetUseSlotSpellCooldownTime(4, owner, false);
                }
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppression), 1);
            }
        }
    }
}
namespace Buffs
{
    public class OdinCaptureChannelBomb : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "",
            BuffTextureName = "",
        };
        float channelStartTime;
        public override void OnActivate()
        {
            this.channelStartTime = GetBuffStartTime(owner, nameof(Buffs.OdinCaptureChannelBomb));
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(attacker is Champion)
            {
                if(damageSource == DamageSource.DAMAGE_SOURCE_PERIODIC)
                {
                }
                else
                {
                    string buffName;
                    float damageStartTime;
                    bool cancelChannel;
                    buffName = GetDamagingBuffName();
                    damageStartTime = GetBuffStartTime(owner, buffName);
                    cancelChannel = false;
                    if(damageStartTime == 0)
                    {
                        cancelChannel = true;
                    }
                    if(damageStartTime >= this.channelStartTime)
                    {
                        cancelChannel = true;
                    }
                    if(cancelChannel)
                    {
                        Particle asdf; // UNUSED
                        SpellEffectCreate(out asdf, out _, "Ezreal_essenceflux_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
                        IssueOrder(owner, OrderType.OrderNone, default, owner);
                        SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
    }
}