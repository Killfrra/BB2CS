#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptredDragon : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float nextBuffVars_HPPerLevel;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DragonVisionBuff)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.DragonVisionBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 100000);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                nextBuffVars_HPPerLevel = 125;
                AddBuff((ObjAIBase)owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int nextBuffVars_TickDamage;
            damageAmount *= 1.43f;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
            {
                if(target != unit)
                {
                    nextBuffVars_TickDamage = 15;
                    AddBuff(attacker, unit, new Buffs.Burning(nextBuffVars_TickDamage), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 1);
                }
            }
        }
    }
}