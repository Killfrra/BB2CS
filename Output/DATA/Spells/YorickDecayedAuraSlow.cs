#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickDecayedAuraSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float attackSpeedMod;
        float moveSpeedMod;
        public YorickDecayedAuraSlow(float attackSpeedMod = default, float moveSpeedMod = default)
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
            ApplyAssistMarker(attacker, target, 10);
        }
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickDecayedSlow)) > 0)
            {
            }
            else
            {
                IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
            }
        }
    }
}