#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RocketJumpSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", },
            BuffName = "RocketJumpSlow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float attackSpeedMod;
        float moveSpeedMod;
        public RocketJumpSlow(float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void UpdateBuffs()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, -0.6f);
        }
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
        }
    }
}