#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pacified : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "Pacified",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        public override void OnActivate()
        {
            SetPacified(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetPacified(owner, false);
        }
    }
}