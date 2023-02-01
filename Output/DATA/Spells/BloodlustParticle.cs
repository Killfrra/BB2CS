#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BloodlustParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Blood Lust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
            SpellFXOverrideSkins = new[]{ "TryndamereDemonsword", },
        };
        Particle particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "bloodLust_flame.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, this.particle, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
    }
}