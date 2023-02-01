#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTwistedBlueWraith : BBCharScript
    {
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, 1);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LifestealAttack)) > 0)
            {
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.LifestealAttack(), 1, 1, 9999, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            damageAmount *= 1.43f;
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
        }
    }
}