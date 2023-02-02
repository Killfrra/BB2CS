#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ExplosiveCask : BBSpellScript
    {
        int[] effect0 = {400, 600, 800};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            AttackableUnit unit; // UNITIALIZED
            targetPos = GetCastSpellTargetPos();
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, ))
            {
                AddBuff(attacker, unit, new Buffs.ExplosiveCask(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0);
            }
            ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, default, false, false);
        }
    }
}
namespace Buffs
{
    public class ExplosiveCask : BBBuffScript
    {
    }
}