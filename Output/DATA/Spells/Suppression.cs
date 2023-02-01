#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Suppression : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Suppression",
            BuffTextureName = "GSB_Blind.dds",
            PopupMessage = new[]{ "game_floatingtext_Suppressed", },
        };
        public override void OnActivate()
        {
            SetSuppressed(owner, true);
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSuppressed(owner, false);
            SetStunned(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetSuppressed(owner, true);
            SetStunned(owner, true);
        }
    }
}