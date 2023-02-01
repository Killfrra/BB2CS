#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiUnstableGrowthRoot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "MaokaiUnstableGrowthRoot",
            BuffTextureName = "GreenTerror_SpikeSlam.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        Particle rootParticleEffect2;
        Particle rootParticleEffect;
        public override void OnActivate()
        {
            TeamId teamOfOwner; // UNUSED
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.rootParticleEffect2, out _, "maokai_elementalAdvance_root_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out this.rootParticleEffect, out _, "maokai_elementalAdvance_root_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(this.rootParticleEffect2);
            SpellEffectRemove(this.rootParticleEffect);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}