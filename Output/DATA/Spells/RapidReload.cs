#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RapidReload : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "RapidReload",
            BuffTextureName = "Corki_RapidReload.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float trueDamageAmount;
            float temp1;
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    trueDamageAmount = 0.1f * damageAmount;
                    temp1 = GetHealth(target, PrimaryAbilityResourceType.MANA);
                    if(trueDamageAmount > temp1)
                    {
                        trueDamageAmount = temp1 - 1;
                        ApplyDamage(attacker, target, trueDamageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false);
                    }
                    else
                    {
                        ApplyDamage(attacker, target, trueDamageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false);
                    }
                }
            }
        }
    }
}