#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Silence : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Silence.troy", },
            BuffName = "Silence",
            BuffTextureName = "Voidwalker_Spellseal.dds",
            PopupMessage = new[]{ "game_floatingtext_Silenced", },
        };
        public override void OnActivate()
        {
            SetSilenced(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSilenced(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetSilenced(owner, true);
        }
    }
}