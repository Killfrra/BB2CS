#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptMasterYi : BBCharScript
    {
        public override void OnUpdateActions()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                float cooldown;
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(cooldown <= 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WujuStyle)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.WujuStyle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DoubleStrikeIcon)) == 0)
            {
                int dSCount;
                AddBuff(attacker, attacker, new Buffs.DoubleStrike(), 7, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
                dSCount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.DoubleStrike));
                if(dSCount >= 7)
                {
                    AddBuff(attacker, attacker, new Buffs.DoubleStrikeIcon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.DoubleStrike), 7);
                    OverrideAutoAttack(0, SpellSlotType.ExtraSlots, attacker, 1, true);
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.AlphaStrike))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AlphaStrike(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, attacker, new Buffs.DoubleStrike(), 7, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MasterYiWujuDeactivated(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}