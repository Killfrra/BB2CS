#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptJarvanIV : BBCharScript
    {
        int[] effect0 = {10, 3, 3, 3, 3};
        float[] effect1 = {0.1f, 0.03f, 0.03f, 0.03f, 0.03f};
        public override void OnUpdateActions()
        {
            float bonusAD;
            float bonusAD2;
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD2 = bonusAD * 1.2f;
            bonusAD *= 1.5f;
            SetSpellToolTipVar(bonusAD2, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            SetSpellToolTipVar(bonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnPreAttack()
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.JarvanIVMartialCadenceCheck)) > 0)
                    {
                        RemoveOverrideAutoAttack(owner, true);
                    }
                    else
                    {
                        OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
                    }
                }
                else
                {
                    RemoveOverrideAutoAttack(owner, true);
                }
            }
            else
            {
                RemoveOverrideAutoAttack(owner, true);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.JarvanIVMartialCadence(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            float armorBoost;
            float attackSpeedBoost;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armorBoost = this.effect0[level];
            attackSpeedBoost = this.effect1[level];
            if(slot == 2)
            {
                IncPermanentFlatArmorMod(owner, armorBoost);
                IncPermanentPercentAttackSpeedMod(owner, attackSpeedBoost);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}