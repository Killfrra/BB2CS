#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearPassiveHealCheck : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Blitzcrank_ManaBarrier.dds",
            NonDispellable = true,
            OnPreDamagePriority = 8,
            PersistsThroughDeath = true,
        };
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentHealth;
            float maxHealth;
            float remainingHealth;
            float percentHealthRemaining;
            float duration;
            float remainingDuration;
            currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            remainingHealth = currentHealth - damageAmount;
            percentHealthRemaining = remainingHealth / maxHealth;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VolibearPassiveCD)) == 0)
            {
                if(percentHealthRemaining <= 0.3f)
                {
                    duration = 6;
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VolibearPassiveHeal)) > 0)
                    {
                        remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.VolibearPassiveHeal));
                        duration += remainingDuration;
                    }
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearPassiveHeal(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearPassiveCD(), 1, 1, 120, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.VolibearPassiveHealCheck), (ObjAIBase)owner, 0);
                }
            }
        }
    }
}