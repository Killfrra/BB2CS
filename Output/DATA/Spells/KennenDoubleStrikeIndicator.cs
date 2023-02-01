#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenDoubleStrikeIndicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "KennenDoubleStrikeIndicator",
            BuffTextureName = "Kennen_ElectricalSurge_Charging.dds",
        };
    }
}