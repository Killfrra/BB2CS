#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TaricHammerInternal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", },
            AutoBuffActivateEffect = new[]{ "Taric_HammerInternal.troy", },
            BuffName = "TaricHammerInternal",
            BuffTextureName = "",
            SpellFXOverrideSkins = new[]{ "", },
        };
        Particle particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "Taric_HammerInternal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
    }
}