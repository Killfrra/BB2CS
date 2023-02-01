#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CassiopeiaNoxiousBlastHaste : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Interventionspeed_buf.troy", },
            BuffName = "CassiopeiaNoxiousBlastHaste",
            BuffTextureName = "Cassiopeia_NoxiousBlast.dds",
        };
        float moveSpeedMod;
        public CassiopeiaNoxiousBlastHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}