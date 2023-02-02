#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptGragas : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                float attackDamage;
                attackDamage = GetTotalAttackDamage(owner);
                attackDamage *= 0.66f;
                SetSpellToolTipVar(attackDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.GragasPassiveHeal(), 1, 1, 4.3f, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.GragasHappyHour(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}