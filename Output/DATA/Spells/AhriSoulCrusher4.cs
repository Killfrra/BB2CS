#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriSoulCrusher4 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "PotionofElusiveness_itm.troy", "PotionofBrilliance_itm.troy", "PotionofGiantStrength_itm.troy", },
            BuffName = "AhriSoulCrusher",
            BuffTextureName = "3017_Egitai_Twinsoul.dds",
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            charVars.FoxFireIsActive = 0;
        }
    }
}