#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneInquisitionSpeedPartNormal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle speedParticle;
        public override void OnActivate()
        {
            Particle speedParticle;
            SpellEffectCreate(out speedParticle, out _, "vayne_passive_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, default, default, false);
            this.speedParticle = speedParticle;
        }
        public override void OnDeactivate(bool expired)
        {
            Particle speedParticle; // UNUSED
            speedParticle = this.speedParticle;
            SpellEffectRemove(this.speedParticle);
        }
    }
}