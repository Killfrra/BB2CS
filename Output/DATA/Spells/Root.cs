#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Root : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Root",
            BuffTextureName = "LuxCrashingBlitz2.dds",
        };
        public override void OnActivate()
        {
            SetRooted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetRooted(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetRooted(owner, true);
        }
    }
}