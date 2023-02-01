#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Cripple : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Cripple",
            BuffTextureName = "48thSlave_Enfeeble.dds",
        };
        float attackSpeedMod;
        public Cripple(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            ApplyAssistMarker(attacker, target, 10);
        }
    }
}