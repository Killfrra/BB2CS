#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTwistedFate : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {55, 80, 105, 130, 155};
        float[] effect1 = {-0.03f, -0.06f, -0.09f, -0.12f, -0.15f};
        float[] effect2 = {0.03f, 0.06f, 0.09f, 0.12f, 0.15f};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(4, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SecondSight(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.SecondSight(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.Count = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 2)
            {
                int nextBuffVars_BonusDamage;
                float nextBuffVars_CooldownBonus;
                float nextBuffVars_AttackSpeedBonus;
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_BonusDamage = this.effect0[level];
                nextBuffVars_CooldownBonus = this.effect1[level];
                nextBuffVars_AttackSpeedBonus = this.effect2[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.CardmasterStack(nextBuffVars_BonusDamage, nextBuffVars_CooldownBonus, nextBuffVars_AttackSpeedBonus), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}