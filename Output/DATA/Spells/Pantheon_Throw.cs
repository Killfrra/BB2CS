#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Pantheon_Throw : BBSpellScript
    {
        int[] effect0 = {65, 105, 145, 185, 225};
        public override void SelfExecute()
        {
            int count;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                    count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                    if(count >= 4)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                    }
                }
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float atkDmg;
            float baseDamage;
            float throwDmg;
            float bonusDamage;
            float finalDamage;
            float maxHP;
            float currentHP;
            float critHealth;
            atkDmg = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            throwDmg = this.effect0[level];
            bonusDamage = atkDmg - baseDamage;
            bonusDamage *= 1.4f;
            finalDamage = bonusDamage + throwDmg;
            maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            currentHP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            critHealth = maxHP * 0.15f;
            if(currentHP <= critHealth)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_CertainDeath)) > 0)
                {
                    finalDamage *= 1.5f;
                }
            }
            ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
        }
    }
}