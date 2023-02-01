#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptAshe : BBCharScript
    {
        float[] effect0 = {0.03f, 0.03f, 0.03f, 0.06f, 0.06f, 0.06f, 0.09f, 0.09f, 0.09f, 0.12f, 0.12f, 0.12f, 0.15f, 0.15f, 0.15f, 0.18f, 0.18f, 0.18f};
        public override void OnUpdateStats()
        {
            float critToAdd;
            critToAdd = charVars.NumSecondsSinceLastCrit * charVars.CritPerSecond;
            IncFlatCritChanceMod(owner, critToAdd);
        }
        public override void OnUpdateActions()
        {
            float aD;
            if(ExecutePeriodically(3, ref charVars.LastCrit, false))
            {
                charVars.NumSecondsSinceLastCrit++;
            }
            if(owner.IsDead)
            {
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ArchersMark)) > 0)
                {
                }
                else
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level > 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.ArchersMark(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
                    }
                }
            }
            aD = GetTotalAttackDamage(owner);
            SetSpellToolTipVar(aD, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void SetVarsByLevel()
        {
            charVars.CritPerSecond = this.effect0[level];
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    charVars.LastCrit = GetTime();
                    charVars.NumSecondsSinceLastCrit = 0;
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string tempName;
            tempName = GetSpellName();
            if(tempName == nameof(Spells.EnchantedCrystalArrow))
            {
                charVars.CastPoint = GetUnitPosition(owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Focus(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.BowMasterFocusDisplay(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}