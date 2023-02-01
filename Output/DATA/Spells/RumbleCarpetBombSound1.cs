#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleCarpetBombSound1 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RumbleDangerZone",
            BuffTextureName = "Annie_GhastlyShield.dds",
        };
        Particle temp;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.temp, out _, "RumbleCarpetBombSoundStart.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.temp);
        }
    }
}