#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenRidiculousProc : BBBuffScript
    {
        float[] effect0 = {0.6f, 0.7f, 0.8f, 0.9f, 1};
        public override void OnDeactivate(bool expired)
        {
            int level;
            float damageMods;
            float attackDamage;
            float superDamage;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageMods = this.effect0[level];
            attackDamage = GetTotalAttackDamage(attacker);
            superDamage = attackDamage * damageMods;
            ApplyDamage(attacker, target, superDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}