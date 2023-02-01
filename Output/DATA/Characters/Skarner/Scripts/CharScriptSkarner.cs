#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptSkarner : BBCharScript
    {
        public override void OnUpdateActions()
        {
            float aD;
            float bonusDamage;
            aD = GetFlatPhysicalDamageMod(owner);
            bonusDamage = aD * 0.8f;
            SetSpellToolTipVar(bonusDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float cDR;
            float cooldown;
            float newCooldown;
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        if(target is not BaseTurret)
                        {
                            if(target is Champion)
                            {
                                cDR = 1;
                            }
                            else
                            {
                                cDR = 0.5f;
                            }
                            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if(cooldown > 0)
                            {
                                newCooldown = cooldown - cDR;
                                SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                            }
                            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if(cooldown > 0)
                            {
                                newCooldown = cooldown - cDR;
                                SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                            }
                            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if(cooldown > 0)
                            {
                                newCooldown = cooldown - cDR;
                                SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                            }
                            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if(cooldown > 0)
                            {
                                newCooldown = cooldown - cDR;
                                SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int slot;
            slot = GetSpellSlot();
            if(slot == 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SkarnerImpaleFlashLock(), 1, 1, 0.25f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            float aD;
            float bonusDamage;
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.SkarnerBrushCheck(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            aD = GetFlatPhysicalDamageMod(owner);
            bonusDamage = aD * 0.8f;
            SetSpellToolTipVar(bonusDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}