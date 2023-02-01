#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LightstrikerDamageBuff : BBBuffScript
    {
        bool willRemove;
        public override void OnActivate()
        {
            this.willRemove = false;
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            ApplyDamage(attacker, target, 90, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0);
            this.willRemove = true;
        }
        public override void OnBeingDodged()
        {
            DebugSay(owner, "Gasp?");
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}