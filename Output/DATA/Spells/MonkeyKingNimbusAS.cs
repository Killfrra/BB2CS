#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingNimbusAS : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingNimbusAS",
            BuffTextureName = "MonkeyKingNimbusStrike.dds",
        };
        float attackSpeedVar;
        public MonkeyKingNimbusAS(float attackSpeedVar = default)
        {
            this.attackSpeedVar = attackSpeedVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedVar);
            IncPercentAttackSpeedMod(owner, this.attackSpeedVar);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedVar);
        }
    }
}