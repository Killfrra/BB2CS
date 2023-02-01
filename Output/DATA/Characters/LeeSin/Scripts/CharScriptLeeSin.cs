#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptLeeSin : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float bonusAD;
            float bonusAD200;
            float bonusAD9;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                bonusAD = GetFlatPhysicalDamageMod(owner);
                bonusAD200 = bonusAD * 2;
                bonusAD9 = bonusAD * 0.9f;
                SetSpellToolTipVar(bonusAD9, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                SetSpellToolTipVar(bonusAD, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                SetSpellToolTipVar(bonusAD200, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.BlindMonkRKick))
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.BlindMonkRRoot(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BlindMonkPassive(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}