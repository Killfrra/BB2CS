#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CamouflageBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CamoflagueBuff",
            BuffTextureName = "BlindMonk_BlindingStrike.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.4f);
        }
        public override void OnActivate()
        {
            IncPercentAttackSpeedMod(owner, 0.4f);
        }
    }
}