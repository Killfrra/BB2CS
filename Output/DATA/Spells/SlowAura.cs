#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SlowAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Slow Aura",
            BuffTextureName = "Chronokeeper_Slow.dds",
        };
        float slowPercent;
        public SlowAura(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.slowPercent);
        }
    }
}