#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGuardianRegenManager : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float previousTakeDamageTime;
        int dealtDamage;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.previousTakeDamageTime = GetGameTime();
            this.dealtDamage = 0;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                float currentTime;
                float timePassed;
                currentTime = GetGameTime();
                timePassed = currentTime - this.previousTakeDamageTime;
                if(timePassed >= 10)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianRegen(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    this.previousTakeDamageTime = currentTime;
                }
                if(this.dealtDamage == 0)
                {
                    if(timePassed > 0.5f)
                    {
                        float myMaxHealth;
                        float healthToDecreaseBy;
                        this.dealtDamage = 1;
                        myMaxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                        healthToDecreaseBy = 0.6f * myMaxHealth;
                        ApplyDamage((ObjAIBase)owner, owner, healthToDecreaseBy, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            SpellBuffRemoveStacks(owner, owner, nameof(Buffs.OdinGuardianRegen), 0);
            this.previousTakeDamageTime = GetGameTime();
        }
    }
}