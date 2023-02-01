#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XenZhaoParryCast : BBSpellScript
    {
        int[] effect0 = {150, 200, 250};
        public override void SelfExecute()
        {
            float dtD;
            Particle a; // UNUSED
            AttackableUnit unit; // UNITIALIZED
            float weaponDmg;
            float weaponDmgBonus;
            float dtDReal;
            float nextBuffVars_Count;
            dtD = this.effect0[level];
            SpellEffectCreate(out a, out _, default, default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, default, default, false);
            weaponDmg = GetTotalAttackDamage(owner);
            weaponDmgBonus = weaponDmg * 0.4f;
            dtDReal = dtD + weaponDmgBonus;
            nextBuffVars_Count = 0;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                if(unit is Champion)
                {
                    nextBuffVars_Count++;
                }
            }
        }
    }
}