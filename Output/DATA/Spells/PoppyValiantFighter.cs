#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyValiantFighter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyValiantFighter",
            BuffTextureName = "Poppy_ValiantFighter.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float healthCurrent;
            float damageSoftCap;
            float damageManipulator;
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                if(attacker is not BaseTurret)
                {
                    healthCurrent = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                    damageSoftCap = 0.1f * healthCurrent;
                    damageManipulator = damageAmount;
                    if(damageManipulator > damageSoftCap)
                    {
                        damageManipulator -= damageSoftCap;
                        damageManipulator *= 0.5f;
                        damageAmount = damageSoftCap + damageManipulator;
                    }
                }
            }
        }
    }
}