#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaQuickSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.troy", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float attackSpeedMod;
        float moveSpeedMod;
        object level;
        float startTime;
        float[] effect0 = {-0.3f, -0.4f, -0.5f};
        public OrianaQuickSlow(float attackSpeedMod = default, float moveSpeedMod = default, object level = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
            this.level = level;
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
            this.startTime = GetGameTime();
        }
        public override void OnUpdateStats()
        {
            object level;
            float speedMod;
            float currentTime;
            float elapsedTime;
            float elapsedRatio;
            float totalSpeed;
            level = this.level;
            speedMod = this.effect0[level];
            currentTime = GetGameTime();
            elapsedTime = currentTime - this.startTime;
            elapsedTime = 2 - elapsedTime;
            elapsedRatio = elapsedTime / 2;
            elapsedRatio = Math.Max(elapsedRatio, 0);
            totalSpeed = speedMod * elapsedRatio;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalSpeed);
        }
    }
}