#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.troy", },
            BuffName = "OrianaDissonanceEnemy",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        int level;
        float attackSpeedMod;
        float moveSpeedMod;
        float ticksLeft;
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public OrianaSlow(int level = default, float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.level = level;
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
            //RequireVar(this.level);
            ApplyAssistMarker(attacker, target, 10);
            this.ticksLeft = 8;
        }
        public override void OnUpdateStats()
        {
            int level;
            float speedMod;
            float elapsedRatio;
            float totalSpeed;
            float ticksLeft;
            level = this.level;
            speedMod = this.effect0[level];
            elapsedRatio = this.ticksLeft / 8;
            totalSpeed = speedMod * elapsedRatio;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalSpeed);
            ticksLeft = this.ticksLeft - 1;
            this.ticksLeft = ticksLeft;
        }
    }
}