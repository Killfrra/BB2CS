#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickDeathGripTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", },
            BuffName = "Wall of Pain Slow",
            BuffTextureName = "Lich_WallOfPain.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        Particle rootParticleEffect2;
        Particle rootParticleEffect;
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SpellEffectCreate(out this.rootParticleEffect2, out _, "SwainShadowGraspRootTemp.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.rootParticleEffect, out _, "swain_shadowGrasp_magic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(this.rootParticleEffect2);
            SpellEffectRemove(this.rootParticleEffect);
        }
    }
}