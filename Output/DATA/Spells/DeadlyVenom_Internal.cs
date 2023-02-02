#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeadlyVenom_Internal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        float damageAmount;
        int lastCount;
        Particle particle;
        float lastTimeExecuted;
        public DeadlyVenom_Internal(float damageAmount = default, int lastCount = default)
        {
            this.damageAmount = damageAmount;
            this.lastCount = lastCount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAmount);
            //RequireVar(this.lastCount);
            SpellEffectCreate(out this.particle, out _, "twitch_poison_counter_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.DeadlyVenom)) > 0)
                {
                    int count;
                    float damageToDeal;
                    count = GetBuffCountFromAll(owner, nameof(Buffs.DeadlyVenom));
                    damageToDeal = this.damageAmount * count;
                    ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 0, false, false, attacker);
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.DeadlyVenom_marker)) > 0)
            {
                int count;
                count = GetBuffCountFromAll(owner, nameof(Buffs.DeadlyVenom));
                if(count == 2)
                {
                    SpellEffectRemove(this.particle);
                    SpellEffectCreate(out this.particle, out _, "twitch_poison_counter_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
                }
                if(count == 3)
                {
                    SpellEffectRemove(this.particle);
                    SpellEffectCreate(out this.particle, out _, "twitch_poison_counter_03.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
                }
                if(count == 4)
                {
                    SpellEffectRemove(this.particle);
                    SpellEffectCreate(out this.particle, out _, "twitch_poison_counter_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
                }
                if(count == 5)
                {
                    SpellEffectRemove(this.particle);
                    SpellEffectCreate(out this.particle, out _, "twitch_poison_counter_05.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
                }
                if(count == 6)
                {
                    if(this.lastCount != 6)
                    {
                        SpellEffectRemove(this.particle);
                        SpellEffectCreate(out this.particle, out _, "twitch_poison_counter_06.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
                    }
                }
                this.lastCount = count;
            }
        }
    }
}