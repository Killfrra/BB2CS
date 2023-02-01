#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Nimbleness : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Nimbleness",
            BuffTextureName = "3009_Boots_of_Teleportation.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.1f);
        }
    }
}