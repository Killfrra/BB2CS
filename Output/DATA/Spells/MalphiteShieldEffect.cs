#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MalphiteShieldEffect : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "MalphiteObduracyEffect",
            BuffTextureName = "Malphite_GraniteShield.dds",
            NonDispellable = true,
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        float lastTimeExecuted;
        float oldArmorAmount;
        public override void OnActivate()
        {
            float hPPool;
            hPPool = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            this.shieldHealth = 0.1f * hPPool;
            SpellBuffRemove(owner, nameof(Buffs.MalphiteShieldRemoval), (ObjAIBase)owner);
            IncreaseShield(owner, this.shieldHealth, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteShieldRemoval(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(this.shieldHealth > 0)
            {
                RemoveShield(owner, this.shieldHealth, true, true);
            }
        }
        public override void OnUpdateActions()
        {
            float hPPool;
            if(this.shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MalphiteShieldBeenHit)) == 0)
                {
                    this.oldArmorAmount = this.shieldHealth;
                    hPPool = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                    this.shieldHealth = 0.1f * hPPool;
                    this.oldArmorAmount = this.shieldHealth - this.oldArmorAmount;
                    ModifyShield(owner, this.oldArmorAmount, true, true, true);
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.shieldHealth;
            if(this.shieldHealth >= damageAmount)
            {
                this.shieldHealth -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.shieldHealth;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.shieldHealth;
                this.shieldHealth = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteShieldBeenHit(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}