#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FallenOneTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "FallenOne_tar.troy", },
            BuffName = "FallenOne",
            BuffTextureName = "Lich_DeathRay.dds",
            NonDispellable = true,
        };
    }
}