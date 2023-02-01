#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeathLotusSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DeathLotusSound",
            BuffTextureName = "Katarina_DeathLotus.dds",
        };
        Particle deathLotus;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.deathLotus, out _, "katarinaDeathLotus_indicator_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.deathLotus);
        }
    }
}