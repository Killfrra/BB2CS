#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SuppressCallForHelp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "SuppressCallforHelp",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        public override void OnActivate()
        {
            SetSuppressCallForHelp(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSuppressCallForHelp(owner, false);
        }
    }
}