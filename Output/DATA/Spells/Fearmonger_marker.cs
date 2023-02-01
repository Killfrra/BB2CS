#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Fearmonger_marker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Fearmonger_cas.troy", },
            BuffName = "Drain",
            BuffTextureName = "Fiddlesticks_ConjureScarecrow.dds",
        };
    }
}