#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenWindSlashReady : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RivenWindScarReady",
            BuffTextureName = "RivenWindScar.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
    }
}