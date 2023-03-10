#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxIlluminatingFraulein : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "LuxDebuff.troy", },
            BuffName = "LuxIlluminatingFraulein",
            BuffTextureName = "LuxIlluminatingFraulein.dds",
        };
    }
}