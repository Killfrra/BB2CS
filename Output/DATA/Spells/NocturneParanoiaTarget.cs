#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneParanoiaTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        float sightReduction;
        public NocturneParanoiaTarget(float sightReduction = default)
        {
            this.sightReduction = sightReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.sightReduction);
            //RequireVar(this.spellLevel);
            IncPermanentFlatBubbleRadiusMod(owner, this.sightReduction);
        }
        public override void OnDeactivate(bool expired)
        {
            float sightReduction;
            sightReduction = this.sightReduction * -1;
            IncPermanentFlatBubbleRadiusMod(owner, sightReduction);
        }
    }
}