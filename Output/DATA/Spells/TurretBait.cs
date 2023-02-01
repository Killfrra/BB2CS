#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretBait : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Reinforce",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        float bonusArmor;
        public TurretBait(float bonusArmor = default)
        {
            this.bonusArmor = bonusArmor;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusArmor);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.bonusArmor);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is BaseTurret)
            {
                ApplyTaunt(owner, target, 5);
            }
        }
    }
}