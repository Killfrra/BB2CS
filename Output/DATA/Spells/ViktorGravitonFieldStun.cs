#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorGravitonFieldStun : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "ViktorGravitonStun",
            BuffTextureName = "ViktorGravitonFieldAUG.dds",
            PopupMessage = new[]{ "game_floatingtext_Stunned", },
        };
        public override void OnActivate()
        {
            float duration; // UNUSED
            SetStunned(owner, true);
            PauseAnimation(owner, true);
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.ViktorGravitonFieldStun));
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            PauseAnimation(owner, false);
        }
    }
}