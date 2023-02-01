#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RegenerationRuneBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Regenerationpotion_itm.troy", },
            BuffName = "RegenerationRune",
            BuffTextureName = "Sona_AriaofPerseverance.dds",
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            Particle arr; // UNUSED
            float healthPercent;
            float missingHealthPercent;
            float healthToRestore;
            SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            missingHealthPercent = 1 - healthPercent;
            healthToRestore = 20 * missingHealthPercent;
            healthToRestore = Math.Max(5, healthToRestore);
            IncHealth(owner, healthToRestore, owner);
        }
        public override void OnUpdateActions()
        {
            float healthPercent;
            float missingHealthPercent;
            float healthToRestore;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                missingHealthPercent = 1 - healthPercent;
                healthToRestore = 5 * missingHealthPercent;
                healthToRestore = Math.Max(1, healthToRestore);
                IncHealth(owner, healthToRestore, owner);
            }
        }
    }
}