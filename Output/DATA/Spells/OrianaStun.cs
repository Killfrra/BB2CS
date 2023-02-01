#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaStun : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
        }
    }
}