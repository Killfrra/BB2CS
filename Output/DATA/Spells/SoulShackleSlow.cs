#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SoulShackleSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", },
            BuffName = "SoulShackleSlow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float attackSpeedMod;
        float moveSpeedMod;
        public SoulShackleSlow(float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void UpdateBuffs()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}