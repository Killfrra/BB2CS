#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneParanoiaDashSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        Particle loop;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.loop, out _, "NocturneParanoiaDashSound.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.loop);
        }
    }
}