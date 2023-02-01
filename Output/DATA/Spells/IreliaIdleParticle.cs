#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IreliaIdleParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", "", },
            BuffName = "IreliaIdleParticle",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle particle4;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle4, out _, "irelia_ult_energy_ready.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_BACK_2", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle4);
        }
    }
}