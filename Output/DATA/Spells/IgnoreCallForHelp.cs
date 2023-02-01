#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IgnoreCallForHelp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "IgnoreCallForHelp",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        public override void OnActivate()
        {
            SetIgnoreCallForHelp(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetIgnoreCallForHelp(owner, false);
        }
    }
}