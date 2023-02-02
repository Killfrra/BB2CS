#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OdinCaptureChannel : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 30f,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int hasPrilisasBlessing;
        Particle particleID;
        Particle particleID2;
        int chargeTimePassed;
        bool removeAnim;
        public override void ChannelingStart()
        {
            int prilisasBlessingCount;
            TeamId teamOfOwner;
            float mana;
            float maxMana;
            float pAR_Percent;
            AddBuff(attacker, attacker, new Buffs.OdinCaptureImmobile(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.OdinGuardianSuppression(), 1, 1, 30, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureChannel(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, true);
            this.hasPrilisasBlessing = 0;
            prilisasBlessingCount = GetBuffCountFromAll(owner, nameof(Buffs.PrilisasBlessing));
            if(prilisasBlessingCount > 0)
            {
                this.hasPrilisasBlessing = 1;
                AddBuff((ObjAIBase)owner, target, new Buffs.PrilisasBlessing(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                AddBuff(attacker, target, new Buffs.OdinGuardianSuppressionOrder(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out this.particleID, out this.particleID2, "OdinCaptureBeam.troy", "OdinCaptureBeam.troy", TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.OdinGuardianSuppressionChaos(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out this.particleID, out this.particleID2, "OdinCaptureBeam.troy", "OdinCaptureBeam.troy", TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
            }
            this.chargeTimePassed = 0;
            mana = GetPAR(target, PrimaryAbilityResourceType.MANA);
            maxMana = GetMaxPAR(target, PrimaryAbilityResourceType.MANA);
            pAR_Percent = mana / maxMana;
            if(pAR_Percent > 0.8f)
            {
                if(owner.Team != target.Team)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreNinja(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            PlayAnimation("Channel_WNDUP", 0, owner, true, true, false);
            this.removeAnim = true;
        }
        public override void ChannelingUpdateActions()
        {
            float accumTime; // UNITIALIZED
            float distance;
            if(accumTime >= 1.5f)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinShrineBombBuff)) > 0)
                {
                    TeamId teamOfOwner;
                    teamOfOwner = GetTeamID(owner);
                    if(teamOfOwner == TeamId.TEAM_BLUE)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombTickOrder(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombTickChaos(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    SpellBuffClear(owner, nameof(Buffs.OdinShrineBombBuff));
                }
                if(this.removeAnim)
                {
                    UnlockAnimation(owner, true);
                    PlayAnimation("Channel", 0, owner, true, true, false);
                    this.removeAnim = false;
                }
            }
            distance = DistanceBetweenObjects("Owner", "Target");
            if(distance >= 525)
            {
                SpellBuffClear(owner, nameof(Buffs.OdinShrineBombBuff));
                SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
            }
        }
        public override void ChannelingUpdateStats()
        {
            TeamId teamOfOwner;
            TeamId targetTeam;
            TeamId myTeam;
            if(this.chargeTimePassed == 0)
            {
                float accumTime; // UNITIALIZED
                if(accumTime > 1.5f)
                {
                    this.chargeTimePassed = 1;
                    SpellEffectRemove(this.particleID);
                    SpellEffectRemove(this.particleID2);
                    teamOfOwner = GetTeamID(owner);
                    if(teamOfOwner == TeamId.TEAM_BLUE)
                    {
                        SpellEffectCreate(out this.particleID, out this.particleID2, "OdinCaptureBeamEngaged_red.troy", "OdinCaptureBeamEngaged_green.troy", TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
                    }
                    else
                    {
                        SpellEffectCreate(out this.particleID, out this.particleID2, "OdinCaptureBeamEngaged_green.troy", "OdinCaptureBeamEngaged_red.troy", TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "crystal", default, false, false, false, false, false);
                    }
                }
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannel)) == 0)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
            }
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.OdinGuardianSuppressionOrder)) == 0)
                {
                    StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
                }
            }
            else
            {
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.OdinGuardianSuppressionChaos)) == 0)
                {
                    StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
                }
            }
            targetTeam = GetTeamID(target);
            myTeam = GetTeamID(owner);
            if(myTeam == targetTeam)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Success, ChannelingStopSource.Move);
            }
        }
        public override void ChannelingSuccessStop()
        {
            float cooldownToSet;
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.particleID2);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionChaos), 1);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionOrder), 1);
            if(target.IsDead)
            {
                cooldownToSet = 0;
            }
            else
            {
                SetUseSlotSpellCooldownTime(3, owner, false);
                cooldownToSet = 3;
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureChannelCooldownBuff(), 1, 1, cooldownToSet, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            SpellBuffClear(owner, nameof(Buffs.OdinScoreNinja));
            UnlockAnimation(owner, true);
            if(this.hasPrilisasBlessing == 1)
            {
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.PrilisasBlessing), 1);
            }
            SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
        }
        public override void ChannelingCancelStop()
        {
            float cooldownToSet;
            SpellEffectCreate(out _, out _, "OdinCaptureCancel.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, "spine", default, false, false, false, false, false);
            UnlockAnimation(owner, true);
            SpellEffectRemove(this.particleID);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionChaos), 1);
            SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinGuardianSuppressionOrder), 1);
            SpellEffectRemove(this.particleID2);
            SpellBuffClear(owner, nameof(Buffs.OdinScoreNinja));
            if(target.IsDead)
            {
                cooldownToSet = 0;
            }
            else
            {
                SetUseSlotSpellCooldownTime(3, owner, false);
                cooldownToSet = 3;
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureChannelCooldownBuff(), 1, 1, cooldownToSet, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            if(this.hasPrilisasBlessing == 1)
            {
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.PrilisasBlessing), 1);
            }
            SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
        }
    }
}
namespace Buffs
{
    public class OdinCaptureChannel : BBBuffScript
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
            this.channelStartTime = GetBuffStartTime(owner, nameof(Buffs.OdinCaptureChannel));
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
                        SpellBuffClear(owner, nameof(Buffs.OdinCaptureChannel));
                        IssueOrder(owner, OrderType.Hold, default, owner);
                        AddBuff(attacker, owner, new Buffs.OdinCaptureInterrupt(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}