#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptKogMaw : BBCharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                float totalDamage;
                float baseDamage;
                float bonusDamage;
                float spell3Display;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                totalDamage = GetTotalAttackDamage(owner);
                baseDamage = GetBaseAttackDamage(owner);
                bonusDamage = totalDamage - baseDamage;
                spell3Display = bonusDamage * 0.5f;
                SetSpellToolTipVar(spell3Display, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRAZombie)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickReviveAllySelf)) == 0)
                    {
                        if(!owner.IsDead)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                float cooldown;
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(cooldown <= 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawCausticSpittle)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawCausticSpittle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level == 0)
            {
                level = 1;
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianDisplay(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            PopAllCharacterData(owner);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}