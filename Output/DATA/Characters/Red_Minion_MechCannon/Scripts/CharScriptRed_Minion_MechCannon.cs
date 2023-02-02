#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptRed_Minion_MechCannon : BBCharScript
    {
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TurretShield)) > 0)
            {
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.TurretShield(), 1, 1, 20000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is BaseTurret)
            {
                damageAmount *= 2;
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(damageSource == default)
            {
                if(attacker is BaseTurret)
                {
                    Particle ar; // UNUSED
                    damageAmount *= 0.5f;
                    SpellEffectCreate(out ar, out _, "FeelNoPain_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.PromoteMeBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}