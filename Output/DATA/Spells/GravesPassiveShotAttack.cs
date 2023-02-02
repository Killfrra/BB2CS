#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GravesPassiveShotAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 20, 20, 35, 35, 35, 35, 50, 50, 50, 50, 65, 65, 65, 65, 80, 80, 80};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            float damageToDeal;
            level = GetLevel(attacker);
            teamID = GetTeamID(owner);
            if(target is not BaseTurret)
            {
                float bonusDamage;
                damageToDeal = GetTotalAttackDamage(attacker);
                bonusDamage = this.effect0[level];
                damageToDeal += bonusDamage;
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                damageToDeal *= 0.5f;
                AddBuff(attacker, target, new Buffs.GravesPassiveShotAttack(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, target.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, nameof(Buffs.GravesPassiveShotAttack), false))
                {
                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
                SpellBuffRemove(attacker, nameof(Buffs.GravesPassiveShot), attacker, 0);
                AddBuff(attacker, attacker, new Buffs.GravesPassiveStack(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                damageToDeal = GetTotalAttackDamage(attacker);
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class GravesPassiveShotAttack : BBBuffScript
    {
    }
}