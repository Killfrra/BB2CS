#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BandagetossFlingCaster : BBBuffScript
    {
        Particle particleID;
        public BandagetossFlingCaster(Particle particleID = default)
        {
            this.particleID = particleID;
        }
        public override void OnActivate()
        {
            //RequireVar(this.particleID);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
        }
    }
}