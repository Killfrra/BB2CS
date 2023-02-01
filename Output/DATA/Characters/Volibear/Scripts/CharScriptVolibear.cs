#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptVolibear : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float hPPoolMod;
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, true))
            {
                hPPoolMod = GetFlatHPPoolMod(attacker);
                hPPoolMod *= 0.15f;
                SetSpellToolTipVar(hPPoolMod, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
        }
        public override void OnActivate()
        {
            string name;
            AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearPassiveBuff(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearPassiveHealCheck(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.RegenPercent = 0.3f;
            charVars.RegenTooltip = 30;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                name = GetUnitSkinName(unit);
                if(name == "Zilean")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearHatred(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.VolibearHatredZilean(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearW(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, true);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}