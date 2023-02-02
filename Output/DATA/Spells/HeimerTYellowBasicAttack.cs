#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HeimerTYellowBasicAttack : BBSpellScript
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
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, (ObjAIBase)owner);
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.UpgradeSlow)) > 0)
            {
                SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}