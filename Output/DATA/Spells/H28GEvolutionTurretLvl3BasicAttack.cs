#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class H28GEvolutionTurretLvl3BasicAttack : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float dmg;
            teamID = GetTeamID(owner);
            attacker = GetChampionBySkinName("Heimerdinger", teamID ?? TeamId.TEAM_UNKNOWN);
            dmg = GetTotalAttackDamage(owner);
            if(target is BaseTurret)
            {
                dmg /= 2;
            }
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.35f, 1, false, false);
            AddBuff(attacker, target, new Buffs.UrAniumRoundsHit(), 30, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false);
        }
    }
}