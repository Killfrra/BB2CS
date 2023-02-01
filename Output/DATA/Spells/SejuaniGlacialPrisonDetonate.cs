#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniGlacialPrisonDetonate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "BUFFBONE_GLB_GROUND_LOC", },
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", "", },
            BuffName = "SejuaniGlacialPrison",
            BuffTextureName = "Sejuani_GlacialPrison.dds",
            PopupMessage = new[]{ "game_floatingtext_Stunned", },
        };
        Particle crystalineParticle;
        public override void OnActivate()
        {
            SetStunned(owner, true);
            PauseAnimation(owner, true);
            SpellEffectCreate(out this.crystalineParticle, out _, "sejuani_ult_tar_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "BUFFBONE_GLB_GROUND_LOC", default, attacker, "Bird_head", default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            PauseAnimation(owner, false);
            SpellEffectRemove(this.crystalineParticle);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
            PauseAnimation(owner, true);
        }
    }
}