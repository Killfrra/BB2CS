#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaHymnofValorAuraB : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaHymnofValorAuraB",
            BuffTextureName = "Sona_HymnofValor.dds",
        };
        float aPADBoost;
        public SonaHymnofValorAuraB(float aPADBoost = default)
        {
            this.aPADBoost = aPADBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aPADBoost);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.aPADBoost);
            IncFlatMagicDamageMod(owner, this.aPADBoost);
        }
    }
}