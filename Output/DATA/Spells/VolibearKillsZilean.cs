#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearKillsZilean : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearHatredZilean",
            BuffTextureName = "GSB_stealth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}