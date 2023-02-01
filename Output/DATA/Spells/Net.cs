#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Net : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "Net",
        };
        public override void OnActivate()
        {
            SetNetted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNetted(owner, false);
        }
    }
}