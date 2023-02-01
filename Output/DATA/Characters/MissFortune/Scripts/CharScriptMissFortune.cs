#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptMissFortune : BBCharScript
    {
        public override void OnUpdateActions()
        {
            float bonusAD;
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 0.45f;
            SetSpellToolTipVar(bonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string names;
            names = GetSpellName();
            if(names == nameof(Spells.MissFortuneBulletTime))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneBulletSound(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrut(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IsPirateHunter(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortunePassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}