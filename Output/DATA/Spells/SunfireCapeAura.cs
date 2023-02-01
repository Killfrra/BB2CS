#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SunfireCapeAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "SunfireCapeAura_tar.troy", },
            BuffName = "Sunfire Cape Aura",
            BuffTextureName = "3068_Sunfire_Cape.dds",
        };
    }
}