#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DoranT2Health : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float healthVar;
        public DoranT2Health(float healthVar = default)
        {
            this.healthVar = healthVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healthVar);
            IncPermanentFlatHPPoolMod(owner, this.healthVar);
        }
        public override void OnDeactivate(bool expired)
        {
            float removeHealth;
            removeHealth = this.healthVar * -1;
            IncPermanentFlatHPPoolMod(owner, removeHealth);
        }
    }
}