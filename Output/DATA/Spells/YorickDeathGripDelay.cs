#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickDeathGripDelay : BBBuffScript
    {
        int damageToDeal;
        Vector3 pos;
        int[] effect0 = {5, 7, 9};
        public YorickDeathGripDelay(int damageToDeal = default, Vector3 pos = default)
        {
            this.damageToDeal = damageToDeal;
            this.pos = pos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            //RequireVar(this.pos);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float nextBuffVars_DamageToDeal;
            Vector3 nextBuffVars_Pos;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_DamageToDeal = this.damageToDeal;
            nextBuffVars_Pos = this.pos;
            AddBuff(attacker, owner, new Buffs.YorickDeathGrip(nextBuffVars_DamageToDeal, nextBuffVars_Pos), 50, 1, this.effect0[level], BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0.1f, true, false, false);
        }
    }
}