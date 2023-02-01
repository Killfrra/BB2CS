#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptGangplank : BBCharScript
    {
        public override void OnUpdateActions()
        {
            int level2;
            float cooldown2;
            float attackDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RaiseMoraleTeamBuff)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.RaiseMorale(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            level2 = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level2 > 0)
            {
                cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(cooldown2 > 0)
                {
                }
                else
                {
                    AddBuff(attacker, owner, new Buffs.PirateScurvy(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 1, true, false, false);
                }
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.PirateScurvy(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 1, true, false, false);
            }
            attackDamage = GetTotalAttackDamage(owner);
            SetSpellToolTipVar(attackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnActivate()
        {
            int skinID;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Scurvy(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IsPirate(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            skinID = GetSkinID(owner);
            if(skinID == 4)
            {
                PlayAnimation("gangplank_key", 0, owner, true, false, false);
            }
        }
        public override void OnResurrect()
        {
            int skinID;
            skinID = GetSkinID(owner);
            if(skinID == 4)
            {
                PlayAnimation("gangplank_key", 0, owner, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}