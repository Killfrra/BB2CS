#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptSummoner_Rider_Order : BBCharScript
    {
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TurretShield)) > 0)
            {
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.TurretShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(damageSource == default)
            {
                if(attacker is BaseTurret)
                {
                    damageAmount *= 0.33f;
                    SpellEffectCreate(out _, out _, "FeelNoPain_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                }
            }
        }
    }
}