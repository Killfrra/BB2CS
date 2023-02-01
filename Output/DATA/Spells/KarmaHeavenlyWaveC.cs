#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KarmaHeavenlyWaveC : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {35, 55, 75, 95, 115, 135};
        int[] effect1 = {70, 110, 150, 190, 230, 270};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float regen;
            float karmaAP;
            float aPToAdd;
            float maxHealth;
            float curHealth;
            float missHealth;
            float healthToRestore;
            float baseHealthRestore;
            Particle br; // UNUSED
            Particle ar; // UNUSED
            Particle hitEffet; // UNUSED
            teamID = GetTeamID(owner);
            if(target.Team == attacker.Team)
            {
                ApplyAssistMarker(attacker, target, 10);
                regen = 0.05f;
                karmaAP = GetFlatMagicDamageMod(owner);
                aPToAdd = karmaAP * 0.0002f;
                regen += aPToAdd;
                maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
                missHealth = maxHealth - curHealth;
                healthToRestore = regen * missHealth;
                baseHealthRestore = this.effect0[level];
                healthToRestore += baseHealthRestore;
                IncHealth(target, healthToRestore, owner);
                if(target == attacker)
                {
                    SpellEffectCreate(out br, out _, "karma_heavenlyWave_self_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out ar, out _, "karma_heavenlyWave_ally_heal.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                }
            }
            else
            {
                SpellEffectCreate(out hitEffet, out _, "karma_heavenlyWave_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
            }
        }
    }
}