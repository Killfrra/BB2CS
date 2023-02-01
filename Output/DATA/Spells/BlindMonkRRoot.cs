#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkRRoot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Haste",
            BuffTextureName = "Summoner_haste.dds",
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}