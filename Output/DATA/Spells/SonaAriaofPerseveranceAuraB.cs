#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaAriaofPerseveranceAuraB : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaAriaofPerseveranceAuraB",
            BuffTextureName = "Sona_AriaofPerseverance.dds",
        };
        float aRMRBoost;
        public SonaAriaofPerseveranceAuraB(float aRMRBoost = default)
        {
            this.aRMRBoost = aRMRBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aRMRBoost);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.aRMRBoost);
            IncFlatSpellBlockMod(owner, this.aRMRBoost);
        }
    }
}