#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AbyssalScepterAuraSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Abyssalscepter_itm.troy", },
            BuffName = "Abyssal Scepter Aura",
            BuffTextureName = "3001_Abyssal_Scepter.dds",
        };
    }
}