#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptShyvana : BBCharScript
    {
        float currentPar;
        float lastTimeExecuted;
        float[] effect0 = {0.8f, 0.85f, 0.9f, 0.95f, 1};
        public override void OnUpdateStats()
        {
            this.currentPar = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) > 0)
            {
                this.currentPar = 0;
            }
        }
        public override void OnUpdateActions()
        {
            float rageCount;
            rageCount = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if(rageCount >= 100)
            {
                SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
            }
            else
            {
                SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
                ClearPARColorOverride(owner);
            }
            if(ExecutePeriodically(1.5f, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) == 0)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level >= 1)
                    {
                        IncPAR(owner, 1, PrimaryAbilityResourceType.Other);
                    }
                }
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttack)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttackDragon)) == 0)
                    {
                        SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float spellCD1;
            float spellCD1a;
            float spellCD1b;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttack)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttackDragon)) == 0)
                        {
                            spellCD1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            spellCD1a = spellCD1 + -0.5f;
                            spellCD1b = Math.Max(spellCD1a, 0);
                            SetSlotSpellCooldownTimeVer2(spellCD1b, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                        }
                    }
                }
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.HitCount = 0;
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
        }
        public override void OnResurrect()
        {
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
            IncPAR(owner, this.currentPar, PrimaryAbilityResourceType.Other);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransformDeath)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.ShyvanaTransformDeath), (ObjAIBase)owner, 0);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            float totalAttackDamage;
            float damagePercent;
            float damageToDisplay;
            if(slot == 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaDragonScales(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    IncPAR(owner, 100, PrimaryAbilityResourceType.Other);
                }
            }
            if(slot == 0)
            {
                totalAttackDamage = GetTotalAttackDamage(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    damagePercent = this.effect0[level];
                }
                else
                {
                    damagePercent = 0.8f;
                }
                damageToDisplay = totalAttackDamage * damagePercent;
                SetSpellToolTipVar(damageToDisplay, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}