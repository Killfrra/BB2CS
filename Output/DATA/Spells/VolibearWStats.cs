#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearWStats : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearWStats",
            BuffTextureName = "VolibearW.dds",
        };
        float volibearWAS;
        public VolibearWStats(float volibearWAS = default)
        {
            this.volibearWAS = volibearWAS;
        }
        public override void OnActivate()
        {
            //RequireVar(this.volibearWAS);
            IncPercentAttackSpeedMod(owner, this.volibearWAS);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.volibearWAS);
        }
    }
}