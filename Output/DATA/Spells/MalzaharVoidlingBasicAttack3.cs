#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MalzaharVoidlingBasicAttack3 : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Champion other1;
            float dmg;
            teamID = GetTeamID(owner);
            other1 = GetChampionBySkinName("Malzahar", teamID);
            dmg = GetTotalAttackDamage(owner);
            ApplyDamage(other1, target, dmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
        }
    }
}