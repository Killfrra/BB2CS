#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CardmasterRedCardAOE : BBBuffScript
    {
        int[] effect0 = {30, 45, 60, 75, 90};
        float[] effect1 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        int[] effect2 = {0, 0, 0, 0, 0};
        public override void OnActivate()
        {
            int level;
            float attackDamage;
            float bonusDamage;
            float redCardDamage;
            float nextBuffVars_MoveSpeedMod;
            int nextBuffVars_AttackSpeedMod;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackDamage = GetTotalAttackDamage(attacker);
            bonusDamage = this.effect0[level];
            redCardDamage = attackDamage + bonusDamage;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(owner != unit)
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, redCardDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false);
                }
                else
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false);
                }
                nextBuffVars_MoveSpeedMod = this.effect1[level];
                nextBuffVars_AttackSpeedMod = this.effect2[level];
                AddBuff(attacker, unit, new Buffs.CardmasterSlow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
            }
        }
    }
}