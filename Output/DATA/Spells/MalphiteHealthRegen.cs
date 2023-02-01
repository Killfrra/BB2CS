#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MalphiteHealthRegen : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "MalphiteHealthRegen",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
            NonDispellable = true,
        };
        float healthRegen;
        Particle sandSwirl;
        public MalphiteHealthRegen(float healthRegen = default)
        {
            this.healthRegen = healthRegen;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healthRegen);
            SpellEffectCreate(out this.sandSwirl, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.sandSwirl);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, this.healthRegen);
        }
    }
}