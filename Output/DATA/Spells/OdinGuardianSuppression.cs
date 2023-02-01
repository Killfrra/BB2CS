#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGuardianSuppression : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle particle;
        float startTime;
        TeamId myTeamID;
        TeamId oldMyTeamID;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "odin_suppression.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Crystal", owner.Position, owner, default, default, false, false, false, false, false);
            this.startTime = GetGameTime();
            ApplyStun(owner, owner, 0.5f);
            this.myTeamID = GetTeamID(owner);
            this.oldMyTeamID = this.myTeamID;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppressionOrder));
            SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppressionChaos));
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            int orderChannelCount;
            int chaosChannelBuff;
            orderChannelCount = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionOrder));
            chaosChannelBuff = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionChaos));
            if(orderChannelCount > 0)
            {
                if(chaosChannelBuff > 0)
                {
                    SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppression));
                }
            }
            if(orderChannelCount == 0)
            {
                if(chaosChannelBuff == 0)
                {
                    SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppression));
                }
            }
        }
        public override void OnUpdateActions()
        {
            float currentTime;
            float timePassed;
            int run;
            int chaosChannelBuff;
            int orderChannelCount;
            float totalBuffCount;
            float damageMultiplier;
            int prilisasBlessingCount;
            float totalHP;
            float dtD;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                currentTime = GetGameTime();
                timePassed = currentTime - this.startTime;
                if(timePassed >= 1.5f)
                {
                    run = 1;
                    chaosChannelBuff = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionChaos));
                    orderChannelCount = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionOrder));
                    if(chaosChannelBuff > 0)
                    {
                        if(orderChannelCount > 0)
                        {
                            run = 0;
                        }
                    }
                    if(orderChannelCount > 0)
                    {
                        if(chaosChannelBuff > 0)
                        {
                            run = 0;
                        }
                    }
                    totalBuffCount = Math.Max(orderChannelCount, chaosChannelBuff);
                    if(run == 1)
                    {
                        damageMultiplier = totalBuffCount - 1;
                        damageMultiplier *= 0.4f;
                        prilisasBlessingCount = GetBuffCountFromAll(owner, nameof(Buffs.PrilisasBlessing));
                        if(prilisasBlessingCount > 0)
                        {
                            damageMultiplier++;
                        }
                        else
                        {
                            damageMultiplier++;
                        }
                        totalHP = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                        dtD = totalHP * 0.0294f;
                        dtD *= damageMultiplier;
                        this.myTeamID = GetTeamID(owner);
                        if(this.myTeamID == TeamId.TEAM_NEUTRAL)
                        {
                            if(this.oldMyTeamID != this.myTeamID)
                            {
                                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureSoundEmptying), (ObjAIBase)owner, 0);
                            }
                            this.oldMyTeamID = this.myTeamID;
                            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureSoundFilling(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                            if(chaosChannelBuff > 0)
                            {
                                dtD *= -0.5f;
                            }
                            else
                            {
                                dtD *= 0.5f;
                            }
                        }
                        else
                        {
                            if(this.oldMyTeamID != this.myTeamID)
                            {
                                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureSoundFilling), (ObjAIBase)owner, 0);
                            }
                            this.oldMyTeamID = this.myTeamID;
                            dtD *= -1;
                            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureSoundEmptying(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                        IncPAR(owner, dtD, PrimaryAbilityResourceType.MANA);
                    }
                }
            }
        }
    }
}