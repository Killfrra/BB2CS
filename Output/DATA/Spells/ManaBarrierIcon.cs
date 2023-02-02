#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ManaBarrierIcon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ManaBarrierIcon",
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
            currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            remainingHealth = currentHealth - damageAmount;
            percentHealthRemaining = remainingHealth / maxHealth;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ManaBarrier)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ManaBarrierCooldown)) == 0)
                {
                    if(percentHealthRemaining <= 0.2f)
                    {
                        float twentyPercentHealth;
                        float damageToLetThrough;
                        float damageToBlock;
                        float curMana;
                        float manaShield;
                        float nextBuffVars_ManaShield;
                        float nextBuffVars_amountToSubtract;
                        twentyPercentHealth = 0.2f * maxHealth;
                        damageToLetThrough = currentHealth - twentyPercentHealth;
                        damageToBlock = damageAmount - damageToLetThrough;
                        curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                        manaShield = curMana * 0.5f;
                        if(manaShield >= damageToBlock)
                        {
                            nextBuffVars_ManaShield = manaShield;
                            nextBuffVars_amountToSubtract = damageToBlock;
                            damageAmount -= damageToBlock;
                            SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                            AddBuff((ObjAIBase)owner, owner, new Buffs.ManaBarrier(nextBuffVars_ManaShield, nextBuffVars_amountToSubtract), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                        else
                        {
                            damageAmount -= manaShield;
                            SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                        }
                        AddBuff((ObjAIBase)owner, owner, new Buffs.ManaBarrierCooldown(), 1, 1, 60, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}