#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaZenithBladeBuffChaos : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "leBlanc_displace_AOE_tar.troy", },
            BuffName = "LeonaZenithBladeBuffChaos",
            PersistsThroughDeath = true,
        };
        Particle b;
        public override void OnActivate()
        {
            Vector3 ownerPos;
            ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out this.b, out _, "Leona_ZenithBlade_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, default, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.b);
        }
    }
}