#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OdinObeliskAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float damagePercent;
            TeamId targetTeam;
            float health;
            float damageAmount;
            TeamId myTeam;
            float currentHealthPercent;
            AddBuff(attacker, target, new Buffs.OdinGrdObeliskSuppression(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            damagePercent = 0.15f;
            targetTeam = GetTeamID(target);
            if(targetTeam == TeamId.TEAM_NEUTRAL)
            {
                damagePercent *= 0.5f;
            }
            health = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            damageAmount = health * damagePercent;
            myTeam = GetTeamID(owner);
            if(targetTeam == TeamId.TEAM_NEUTRAL)
            {
                if(myTeam == TeamId.TEAM_BLUE)
                {
                    currentHealthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                    if(currentHealthPercent >= 0.96f)
                    {
                        ApplyDamage(attacker, target, 100000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                    }
                    else
                    {
                        IncHealth(target, damageAmount, owner);
                    }
                }
                else
                {
                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                }
            }
            else
            {
                ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
            }
        }
    }
}