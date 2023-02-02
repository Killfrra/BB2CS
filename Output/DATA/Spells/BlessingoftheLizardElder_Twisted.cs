#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlessingoftheLizardElder_Twisted : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "BlessingoftheLizardElder",
            BuffTextureName = "48thSlave_WaveOfLoathing.dds",
            NonDispellable = true,
        };
        Vector3 particlePosition;
        Particle buffParticle;
        Particle castParticle;
        Region bubble;
        int[] effect0 = {15, 15, 20, 20, 25, 25, 30, 30, 35, 35, 40, 40, 45, 45, 50, 50, 55, 55};
        int[] effect1 = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        float[] effect2 = {-0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f};
        float[] effect3 = {-0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f};
        float[] effect4 = {-0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.2f, -0.2f, -0.2f, -0.2f, -0.2f, -0.2f, -0.3f, -0.3f, -0.3f, -0.3f, -0.3f, -0.3f};
        int[] effect5 = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        public BlessingoftheLizardElder_Twisted(Vector3 particlePosition = default)
        {
            this.particlePosition = particlePosition;
        }
        public override void OnActivate()
        {
            Vector3 particlePosition;
            TeamId teamID;
            //RequireVar(this.particlePosition);
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            particlePosition = this.particlePosition;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.castParticle, out _, "ClairvoyanceEyeLong.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, particlePosition, target, default, default, false, default, default, false);
                this.bubble = AddPosPerceptionBubble(teamID, 2200, particlePosition, 180, default, false);
            }
            if(teamID == TeamId.TEAM_PURPLE)
            {
                SpellEffectCreate(out this.castParticle, out _, "ClairvoyanceEyeLong.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, particlePosition, target, default, default, false, default, default, false);
                this.bubble = AddPosPerceptionBubble(teamID, 2200, particlePosition, 180, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
            SpellEffectRemove(this.castParticle);
            RemovePerceptionBubble(this.bubble);
        }
        public override void OnDeath()
        {
            int count;
            float newDuration;
            Vector3 nextBuffVars_ParticlePosition;
            count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            newDuration = 90;
            nextBuffVars_ParticlePosition = this.particlePosition;
            if(!attacker.IsDead)
            {
                if(attacker is Champion)
                {
                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.15f;
                    }
                    else
                    {
                        if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.Monsterbuffs2)) > 0)
                        {
                            newDuration *= 1.3f;
                        }
                    }
                    AddBuff(attacker, attacker, new Buffs.BlessingoftheLizardElder_Twisted(nextBuffVars_ParticlePosition), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if(count != 0)
            {
                ObjAIBase caster;
                caster = GetPetOwner((Pet)attacker);
                if(caster is Champion)
                {
                    if(!caster.IsDead)
                    {
                        newDuration = 150;
                        if(GetBuffCountFromCaster(caster, caster, nameof(Buffs.MonsterBuffs)) > 0)
                        {
                            newDuration *= 1.15f;
                        }
                        else
                        {
                            if(GetBuffCountFromCaster(caster, caster, nameof(Buffs.Monsterbuffs2)) > 0)
                            {
                                newDuration *= 1.3f;
                            }
                        }
                        AddBuff(caster, caster, new Buffs.BlessingoftheLizardElder_Twisted(nextBuffVars_ParticlePosition), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(owner is Champion)
                    {
                        if(target is ObjAIBase)
                        {
                            if(target is BaseTurret)
                            {
                            }
                            else
                            {
                                int level;
                                float nextBuffVars_TickDamage;
                                float nextBuffVars_attackSpeedMod;
                                float nextBuffVars_MoveSpeedMod;
                                float nextBuffVars_AttackSpeedMod;
                                level = GetLevel(owner);
                                nextBuffVars_TickDamage = this.effect0[level];
                                nextBuffVars_attackSpeedMod = this.effect1[level];
                                AddBuff(attacker, target, new Buffs.Burning(nextBuffVars_TickDamage, nextBuffVars_attackSpeedMod), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 1, true, false, false);
                                if(IsRanged(owner))
                                {
                                    nextBuffVars_MoveSpeedMod = this.effect2[level];
                                }
                                else
                                {
                                    if(GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.effect3[level];
                                    }
                                    else
                                    {
                                        nextBuffVars_MoveSpeedMod = this.effect4[level];
                                    }
                                }
                                nextBuffVars_AttackSpeedMod = this.effect5[level];
                                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}