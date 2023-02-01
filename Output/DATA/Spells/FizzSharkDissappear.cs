#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzSharkDissappear : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
        public override void OnResurrect()
        {
            SetNoRender(owner, false);
            SpellBuffClear(owner, nameof(Buffs.FizzSharkDissappear));
        }
    }
}