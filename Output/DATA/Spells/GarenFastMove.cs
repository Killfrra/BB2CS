#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenFastMove : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "garen_descisiveStrike_speed.troy", "garen_descisiveStrike_indicator.troy", },
            BuffName = "GarenFastMove",
            BuffTextureName = "Garen_DecisiveStrike.dds",
        };
        float speedMod;
        public GarenFastMove(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.speedMod);
        }
    }
}