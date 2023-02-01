#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawVoidOozeSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "global_slow.troy", },
            BuffName = "KogMawVoidOoze",
            BuffTextureName = "KogMaw_VoidOoze.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float slowPercent;
        public KogMawVoidOozeSlow(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.slowPercent);
        }
    }
}