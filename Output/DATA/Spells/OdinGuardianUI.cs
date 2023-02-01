#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGuardianUI : BBBuffScript
    {
        float towerHP;
        public override void OnActivate()
        {
            float towerHP;
            towerHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            this.towerHP = towerHP;
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float percentDamage;
            percentDamage = damageAmount / this.towerHP;
            if(percentDamage >= 0.01f)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianUIDamage(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianUIDamageChaos(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float percentHeal;
            percentHeal = health / this.towerHP;
            if(percentHeal > 0.01f)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianUIDamageOrder(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            return returnValue;
        }
    }
}