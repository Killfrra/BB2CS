#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KennenShurikenThrow : BBSpellScript
    {
        int[] effect0 = {75, 110, 145, 180, 215};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float properDamage;
            properDamage = this.effect0[level];
            BreakSpellShields(target);
            AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 1, false, false, attacker);
        }
    }
}