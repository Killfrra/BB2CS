#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinDragonBuffShield : BBBuffScript
    {
        float totalArmorAmount;
        float oldArmorAmount;
        public override void OnActivate()
        {
            this.totalArmorAmount = 1000;
            IncreaseShield(owner, this.totalArmorAmount, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveShield(owner, this.totalArmorAmount, true, true);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.totalArmorAmount;
            if(this.totalArmorAmount >= damageAmount)
            {
                this.totalArmorAmount -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.totalArmorAmount;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.totalArmorAmount;
                this.totalArmorAmount = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}