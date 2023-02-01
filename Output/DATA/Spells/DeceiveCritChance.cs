#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeceiveCritChance : BBBuffScript
    {
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatCritChanceMod(owner, 1);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            SpellBuffClear(owner, nameof(Buffs.DeceiveCritChance));
        }
    }
}