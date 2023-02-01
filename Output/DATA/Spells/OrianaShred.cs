#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaShred : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        object level;
        float attackSpeedMod;
        float moveSpeedMod;
        int[] effect0 = {-2, -4, -6, -8, -10};
        public OrianaShred(object level = default, float attackSpeedMod = default, float moveSpeedMod = default)
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
        }
        public override void OnUpdateStats()
        {
            object level;
            level = this.level;
            IncFlatSpellBlockMod(owner, this.effect0[level]);
        }
    }
}