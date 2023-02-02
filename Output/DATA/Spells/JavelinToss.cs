#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JavelinToss : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {55, 95, 140, 185, 230};
        public override void SelfExecute()
        {
            float baseDamage;
            float aP;
            float aPDamage;
            float startingDamage;
            baseDamage = this.effect0[level];
            aP = GetFlatMagicDamageMod(owner);
            aPDamage = aP * 0.65f;
            startingDamage = baseDamage + aPDamage;
            charVars.StartingDamage = startingDamage;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool isStealthed;
            TeamId teamID;
            Particle asffa; // UNUSED
            float distance;
            float multiplicant;
            float finalDamage;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                teamID = GetTeamID(owner);
                SpellEffectCreate(out asffa, out _, "nidalee_javelinToss_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                BreakSpellShields(target);
                distance = DistanceBetweenObjects("Target", "Owner");
                multiplicant = distance / 1000;
                multiplicant++;
                multiplicant = Math.Min(multiplicant, 2.5f);
                finalDamage = multiplicant * charVars.StartingDamage;
                ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if(target is Champion)
                {
                    teamID = GetTeamID(owner);
                    SpellEffectCreate(out asffa, out _, "nidalee_javelinToss_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                    BreakSpellShields(target);
                    distance = DistanceBetweenObjects("Target", "Owner");
                    multiplicant = distance / 1000;
                    multiplicant++;
                    multiplicant = Math.Min(multiplicant, 2.5f);
                    finalDamage = multiplicant * charVars.StartingDamage;
                    ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    bool canSee;
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        teamID = GetTeamID(owner);
                        SpellEffectCreate(out asffa, out _, "nidalee_javelinToss_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                        BreakSpellShields(target);
                        distance = DistanceBetweenObjects("Target", "Owner");
                        multiplicant = distance / 1000;
                        multiplicant++;
                        multiplicant = Math.Min(multiplicant, 2.5f);
                        finalDamage = multiplicant * charVars.StartingDamage;
                        ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}