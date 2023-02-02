#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CaitlynPlaceholder : BBBuffScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(RandomChance() < charVars.MiniCritChance)
                    {
                        TeamId teamID;
                        Particle motaExplosion; // UNUSED
                        if(target is not Champion)
                        {
                            damageAmount *= 10;
                            teamID = GetTeamID(attacker);
                            SpellEffectCreate(out motaExplosion, out _, "akali_mark_impact_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                            Say(target, "Mini Crit: ", damageAmount);
                        }
                        else
                        {
                            damageAmount *= 10;
                            teamID = GetTeamID(attacker);
                            SpellEffectCreate(out motaExplosion, out _, "akali_mark_impact_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                            Say(target, "Mini Crit: ", damageAmount);
                        }
                    }
                }
            }
        }
    }
}