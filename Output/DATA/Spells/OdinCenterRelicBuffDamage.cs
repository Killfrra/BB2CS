#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCenterRelicBuffDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterRelicDamage",
            BuffTextureName = "StormShield.dds",
        };
        float totalDamage;
        Particle buffParticle;
        float prevSpellTrigger;
        float lastTimeExecuted;
        Particle particleID; // UNUSED
        public override void OnActivate()
        {
            int level;
            float bonusDamage;
            level = GetLevel(owner);
            bonusDamage = level * 13;
            this.totalDamage = bonusDamage + 36;
            SetBuffToolTipVar(1, this.totalDamage);
            SpellEffectCreate(out this.buffParticle, out _, "odin_relic_buf_light_blue.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            this.prevSpellTrigger = 0;
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID; // UNUSED
            teamID = GetTeamID(owner);
            if(ExecutePeriodically(4, ref this.lastTimeExecuted, true))
            {
                if(owner.IsDead)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID;
            float currentTime;
            float timeDiff;
            float distance;
            teamID = GetTeamID(owner);
            currentTime = GetGameTime();
            timeDiff = currentTime - this.prevSpellTrigger;
            if(owner != target)
            {
                if(timeDiff >= 4)
                {
                    if(target is not BaseTurret)
                    {
                        if(target is ObjAIBase)
                        {
                            if(damageSource != default)
                            {
                                if(damageSource != default)
                                {
                                    if(damageSource != default)
                                    {
                                        distance = DistanceBetweenObjects("Owner", "Target");
                                        if(distance <= 1600)
                                        {
                                            SpellEffectCreate(out this.particleID, out _, "Odin_CenterbuffBeam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, attacker, "head", default, target, "root", default, true, false, false, false, false);
                                            this.prevSpellTrigger = currentTime;
                                            ApplyDamage((ObjAIBase)owner, target, this.totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}