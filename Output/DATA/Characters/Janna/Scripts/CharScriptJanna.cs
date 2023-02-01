#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptJanna : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float cooldown;
            if(!owner.IsDead)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SowTheWind)) == 0)
                    {
                        cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(cooldown <= 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.SowTheWind(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
            if(ExecutePeriodically(4, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.Tailwind(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.TailwindSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.Tailwind(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}