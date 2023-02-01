#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkRGhost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Haste",
            BuffTextureName = "Summoner_haste.dds",
        };
        public override void OnActivate()
        {
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
    }
}