#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathEnergize : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Haste.troy", },
            BuffName = "Haste",
            BuffTextureName = "Chronokeeper_Recall.dds",
        };
        float moveSpeedMod;
        public override void OnActivate()
        {
            this.moveSpeedMod = 0.35f;
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}