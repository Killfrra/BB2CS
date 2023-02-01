#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SiphoningStrike : BBBuffScript
    {
        int[] effect0 = {3, 3, 3, 3, 3};
        public override void OnActivate()
        {
            //RequireVar(this.damageBonus);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeath()
        {
            int level;
            int nextBuffVars_DamageBonus;
            SpellEffectCreate(out _, out _, "DeathsCaress_nova.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(attacker.IsDead)
            {
            }
            else
            {
                nextBuffVars_DamageBonus = this.effect0[level];
                AddBuff(attacker, attacker, new Buffs.SiphoningStrikeDamageBonus(nextBuffVars_DamageBonus), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}