#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ValkyrieSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ValkyrieSound",
            BuffTextureName = "Corki_Valkyrie.dds",
            SpellFXOverrideSkins = new[]{ "UrfRiderCorki", },
        };
    }
}