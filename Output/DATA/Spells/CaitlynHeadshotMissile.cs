#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CaitlynHeadshotMissile : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float damageAmount;
            TeamId teamID;
            Particle motaExplosion; // UNUSED
            damageAmount = GetTotalAttackDamage(owner);
            if(target is not Champion)
            {
                if(hitResult == HitResult.HIT_Critical)
                {
                    damageAmount *= 1.75f;
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out motaExplosion, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
                else
                {
                    damageAmount *= 2.5f;
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out motaExplosion, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
            }
            else
            {
                if(hitResult == HitResult.HIT_Critical)
                {
                    damageAmount *= 1.25f;
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out motaExplosion, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
                else
                {
                    damageAmount *= 1.5f;
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out motaExplosion, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
            }
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.CaitlynHeadshot), (ObjAIBase)owner);
        }
    }
}