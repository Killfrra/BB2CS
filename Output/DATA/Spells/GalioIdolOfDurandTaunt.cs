#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GalioIdolOfDurandTaunt : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Galio_IdolOfDurand.dds",
        };
        Particle tauntVFX;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.tauntVFX, out _, "galio_taunt_unit_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.tauntVFX);
        }
    }
}