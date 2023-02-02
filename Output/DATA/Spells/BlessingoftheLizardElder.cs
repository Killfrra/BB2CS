#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlessingoftheLizardElder : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "BlessingoftheLizardElder",
            BuffTextureName = "48thSlave_WaveOfLoathing.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        int[] effect0 = {10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44};
        int[] effect1 = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        float[] effect2 = {-0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f};
        float[] effect3 = {-0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f};
        float[] effect4 = {-0.08f, -0.08f, -0.08f, -0.08f, -0.08f, -0.08f, -0.16f, -0.16f, -0.16f, -0.16f, -0.16f, -0.16f, -0.24f, -0.24f, -0.24f, -0.24f, -0.24f, -0.24f};
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnDeath()
        {
            int count;
            float newDuration;
            count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            if(attacker is Champion)
            {
                if(!attacker.IsDead)
                {
                    newDuration = 150;
                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(attacker, attacker, new Buffs.BlessingoftheLizardElder(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
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
                            newDuration *= 1.2f;
                        }
                        AddBuff(caster, caster, new Buffs.BlessingoftheLizardElder(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(owner is Champion)
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        if(damageSource == default)
                        {
                            int level;
                            float nextBuffVars_TickDamage;
                            int nextBuffVars_attackSpeedMod;
                            float nextBuffVars_MoveSpeedMod;
                            level = GetLevel(owner);
                            nextBuffVars_TickDamage = this.effect0[level];
                            nextBuffVars_attackSpeedMod = this.effect1[level];
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
                            AddBuff(attacker, target, new Buffs.Burning(nextBuffVars_TickDamage, nextBuffVars_attackSpeedMod), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 1, true, false, false);
                            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}