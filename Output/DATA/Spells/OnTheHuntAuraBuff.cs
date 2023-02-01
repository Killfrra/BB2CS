#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OnTheHuntAuraBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", },
            AutoBuffActivateEffect = new[]{ "OntheHunt_buf.troy", "", },
            BuffName = "On The Hunt",
            BuffTextureName = "Sivir_Deadeye.dds",
        };
        float moveSpeedMod;
        float allyAttackSpeedMod;
        public OnTheHuntAuraBuff(float moveSpeedMod = default, float allyAttackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.allyAttackSpeedMod = allyAttackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.allyAttackSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentAttackSpeedMod(owner, this.allyAttackSpeedMod);
        }
    }
}