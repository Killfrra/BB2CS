#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ChronoShift : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 160f, 140f, 120f, },
        };
        int[] effect0 = {600, 850, 1100};
        int[] effect1 = {7, 7, 7};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float abilityPower;
            float baseHealthBoost;
            float abilityPowerb;
            float abilityPowerMod;
            float healthPlusAbility;
            float nextBuffVars_HealthPlusAbility;
            bool nextBuffVars_WillRemove;
            abilityPower = GetFlatMagicDamageMod(owner);
            baseHealthBoost = this.effect0[level];
            abilityPowerb = abilityPower + 0.1f;
            abilityPowerMod = abilityPowerb * 2;
            healthPlusAbility = abilityPowerMod + baseHealthBoost;
            nextBuffVars_HealthPlusAbility = healthPlusAbility;
            nextBuffVars_WillRemove = false;
            AddBuff(attacker, target, new Buffs.ChronoShift(nextBuffVars_HealthPlusAbility, nextBuffVars_WillRemove), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ChronoShift : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Chrono Shift",
            BuffTextureName = "Chronokeeper_Timetwister.dds",
            NonDispellable = true,
            OnPreDamagePriority = 4,
            PersistsThroughDeath = true,
        };
        float healthPlusAbility;
        bool willRemove;
        Particle asdf;
        public ChronoShift(float healthPlusAbility = default, bool willRemove = default)
        {
            this.healthPlusAbility = healthPlusAbility;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            //RequireVar(this.healthPlusAbility);
            //RequireVar(this.willRemove);
            SpellEffectCreate(out this.asdf, out _, "nickoftime_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.asdf);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth;
            ObjAIBase caster;
            curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            caster = SetBuffCasterUnit();
            if(curHealth <= damageAmount)
            {
                float nextBuffVars_HealthPlusAbility;
                damageAmount = curHealth - 1;
                nextBuffVars_HealthPlusAbility = this.healthPlusAbility;
                this.willRemove = true;
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombie)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieLich)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieKogMaw)) == 0)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAPetBuff2)) == 0)
                            {
                                AddBuff(caster, owner, new Buffs.ChronoRevive(nextBuffVars_HealthPlusAbility), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}