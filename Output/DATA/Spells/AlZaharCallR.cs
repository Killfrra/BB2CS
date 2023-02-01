#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlZaharCallR : BBBuffScript
    {
        Particle particle2;
        Particle particle3;
        public AlZaharCallR(Particle particle2 = default, Particle particle3 = default)
        {
            this.particle2 = particle2;
            this.particle3 = particle3;
        }
        public override void OnActivate()
        {
            //RequireVar(this.particle2);
            //RequireVar(this.particle3);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
        }
    }
}