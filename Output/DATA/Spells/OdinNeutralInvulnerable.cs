#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinNeutralInvulnerable : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "minatuar_unbreakableWill_cas.troy", "feroscioushowl_cas2.troy", },
            BuffName = "JudicatorIntervention",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        Particle particle1;
        public override void OnActivate()
        {
            SetInvulnerable(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
            SetTargetable(owner, false);
            SpellEffectCreate(out this.particle1, out _, "OdinNeutralInvulnerable.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            SpellEffectRemove(this.particle1);
            SetTargetable(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
        }
    }
}