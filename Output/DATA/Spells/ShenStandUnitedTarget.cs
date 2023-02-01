#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShenStandUnitedTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Shen Stand United Target",
            BuffTextureName = "Shen_StandUnited.dds",
        };
        Particle particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "shen_Teleport_target_v2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
    }
}