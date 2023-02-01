#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenewalTunic : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Renewal Shell",
            BuffTextureName = "3051_Renewal_Tunic.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, 4);
        }
    }
}