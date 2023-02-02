#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptIrelia : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                float count;
                count = 0;
                charVars.CCReduction = 1;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    bool canSee;
                    canSee = CanSeeTarget(owner, unit);
                    if(canSee)
                    {
                        count++;
                    }
                }
                SpellBuffClear(owner, nameof(Buffs.IreliaIonianDuelist));
                if(count == 1)
                {
                    charVars.CCReduction += -0.1f;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIonianDuelist(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
                if(count == 2)
                {
                    charVars.CCReduction += -0.25f;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIonianDuelist(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIonianDuelist(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
                if(count >= 3)
                {
                    charVars.CCReduction += -0.4f;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIonianDuelist(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIonianDuelist(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIonianDuelist(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnUpdateActions()
        {
            float bonusAD;
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 0.6f;
            SetSpellToolTipVar(bonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIdleParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.IreliaHitenStyleCharged)) > 0)
                {
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaHitenStyle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptIrelia : BBBuffScript
    {
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                float percentReduction; // UNITIALIZED
                if(type == BuffType.SNARE)
                {
                    duration *= charVars.CCReduction;
                }
                if(type == BuffType.SLOW)
                {
                    duration *= charVars.CCReduction;
                }
                if(type == BuffType.FEAR)
                {
                    duration *= charVars.CCReduction;
                }
                if(type == BuffType.CHARM)
                {
                    duration *= charVars.CCReduction;
                }
                if(type == BuffType.SLEEP)
                {
                    duration *= charVars.CCReduction;
                }
                if(type == BuffType.STUN)
                {
                    duration *= charVars.CCReduction;
                }
                if(type == BuffType.TAUNT)
                {
                    duration *= charVars.CCReduction;
                }
                if(type == BuffType.SILENCE)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.BLIND)
                {
                    duration *= percentReduction;
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
    }
}