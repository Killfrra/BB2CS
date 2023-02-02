#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MonsterRegenSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 0f, 0f, 0f, 0f, 0f, },
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle ar; // UNUSED
            Particle arr; // UNUSED
            float healthPercent;
            float missingHealthPercent;
            float healthToRestore;
            teamID = GetTeamID(target);
            SpellEffectCreate(out ar, out _, "VampHeal.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
            missingHealthPercent = 1 - healthPercent;
            healthToRestore = 60 * missingHealthPercent;
            healthToRestore = Math.Max(10, healthToRestore);
            IncHealth(target, healthToRestore, target);
        }
    }
}