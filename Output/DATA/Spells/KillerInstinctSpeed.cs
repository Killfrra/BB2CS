#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KillerInstinctSpeed : BBBuffScript
    {
        object level;
        Particle kISpeed;
        int[] effect0 = {4, 5, 6, 7, 8};
        public KillerInstinctSpeed(object level = default, Particle kISpeed = default)
        {
            this.level = level;
            this.kISpeed = kISpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.kISpeed);
        }
        public override void OnUpdateStats()
        {
            object level;
            float attackDamageBoon;
            level = this.level;
            attackDamageBoon = this.effect0[level];
            IncFlatPhysicalDamageMod(owner, attackDamageBoon);
        }
    }
}