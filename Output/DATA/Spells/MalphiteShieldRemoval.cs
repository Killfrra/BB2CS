#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MalphiteShieldRemoval : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle sEPar;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.sEPar, out _, "Obduracy_off.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, "root", default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.sEPar);
        }
    }
}