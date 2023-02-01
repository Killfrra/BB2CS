#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaZenithBladeRoot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeonaZenithBladeRoot",
            BuffTextureName = "LeonaZenithBlade.dds",
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