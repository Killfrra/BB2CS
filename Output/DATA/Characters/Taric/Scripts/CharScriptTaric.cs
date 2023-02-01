#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTaric : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float shatterCD;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
                {
                    if(owner.IsDead)
                    {
                    }
                    else
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            AddBuff(attacker, unit, new Buffs.ShatterAura(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        shatterCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(shatterCD <= 0)
                        {
                            AddBuff(attacker, target, new Buffs.ShatterSelfBonus(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float cooldown;
            float newCooldown;
            if(hitResult != HitResult.HIT_Miss)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level > 0)
                    {
                        cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(cooldown > 0)
                        {
                            if(target is Champion)
                            {
                                newCooldown = cooldown - 3;
                                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
                            }
                            else
                            {
                                newCooldown = cooldown - 1;
                                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
                            }
                        }
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int temp;
            temp = GetSpellSlot();
            if(temp == 3)
            {
                AddBuff(attacker, owner, new Buffs.TaricHammerInternal(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Gemcraft(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}