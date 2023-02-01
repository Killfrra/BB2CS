#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Swashbuckler : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Swashbuckler",
            BuffTextureName = "MasterYi_DoubleStrike.dds",
            NonDispellable = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float targetHealth;
            if(target is ObjAIBase)
            {
                if(default is BaseTurret)
                {
                }
                else
                {
                    targetHealth = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                    if(targetHealth <= 0.3f)
                    {
                        damageAmount *= 1.3f;
                    }
                }
            }
        }
    }
}