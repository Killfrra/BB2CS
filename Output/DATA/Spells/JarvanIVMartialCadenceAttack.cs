#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JarvanIVMartialCadenceAttack : BBSpellScript
    {
        float[] effect0 = {0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.08f, 0.08f, 0.08f, 0.08f, 0.08f, 0.08f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float healthPerc;
            float targetHealth;
            float healthDamage;
            float baseDamage;
            Particle c; // UNUSED
            teamID = GetTeamID(owner);
            level = GetLevel(owner);
            healthPerc = this.effect0[level];
            targetHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            healthDamage = targetHealth * healthPerc;
            healthDamage = Math.Min(400, healthDamage);
            healthDamage = Math.Max(2, healthDamage);
            ApplyDamage(attacker, target, healthDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.JarvanIVMartialCadenceCheck(), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out c, out _, "jarvincritattack_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            RemoveOverrideAutoAttack(owner, false);
        }
    }
}