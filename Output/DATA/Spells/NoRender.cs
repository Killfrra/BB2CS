#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NoRender : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NoRender",
        };
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
        }
    }
}