#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EmblemOfValor : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Emblem of Valour",
            BuffTextureName = "3052_Reverb_Coil.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, 2);
        }
    }
}