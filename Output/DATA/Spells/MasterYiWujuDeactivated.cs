#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MasterYiWujuDeactivated : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle glowblade;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.glowblade, out _, "yiglowblade.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weaponstreak", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.glowblade);
        }
    }
}