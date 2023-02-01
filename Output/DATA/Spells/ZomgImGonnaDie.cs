#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ZomgImGonnaDie : BBBuffScript
    {
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth;
            TeamId teamID;
            Particle arm8; // UNUSED
            curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(curHealth <= damageAmount)
            {
                teamID = GetTeamID(owner);
                SpellEffectCreate(out arm8, out _, "teleportarrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                if(teamID == TeamId.TEAM_BLUE)
                {
                    TeleportToKeyLocation(owner, SpawnType.SPAWN_LOCATION, TeamId.TEAM_BLUE);
                }
                else if(true)
                {
                    TeleportToKeyLocation(attacker, SpawnType.SPAWN_LOCATION, TeamId.TEAM_PURPLE);
                }
            }
            damageAmount = 0;
        }
    }
}