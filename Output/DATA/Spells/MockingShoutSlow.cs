#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MockingShoutSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "head", },
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", "Chicken_buf.troy", },
            BuffName = "Mocking Shout Slow",
            BuffTextureName = "48thSlave_Pacify.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        float attackSpeedMod;
        public MockingShoutSlow(float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void UpdateBuffs()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}