#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptCaitlyn : BBCharScript
    {
        int[] effect0 = {8, 8, 8, 8, 8, 8, 7, 7, 7, 7, 7, 7, 6, 6, 6, 6, 6, 6};
        int[] effect1 = {6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4};
        public override void OnUpdateActions()
        {
            float bonusAD;
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 2;
            SetSpellToolTipVar(bonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void SetVarsByLevel()
        {
            charVars.TooltipAmount = this.effect0[level];
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.CaitlynHeadshot)) == 0)
                    {
                        bool isInBrush;
                        isInBrush = IsInBrush(attacker);
                        if(isInBrush)
                        {
                            AddBuff(attacker, attacker, new Buffs.CaitlynHeadshotCount(), 8, 2, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff(attacker, attacker, new Buffs.CaitlynHeadshotCount(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                    }
                    else
                    {
                        if(target is ObjAIBase)
                        {
                            if(target is BaseTurret)
                            {
                            }
                            else
                            {
                                RemoveOverrideAutoAttack(owner, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnPreAttack()
        {
            int brushCount;
            bool isInBrush;
            level = GetLevel(owner);
            brushCount = this.effect1[level];
            isInBrush = IsInBrush(attacker);
            if(isInBrush)
            {
                int count;
                count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.CaitlynHeadshotCount));
                if(count >= brushCount)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CaitlynHeadshot(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.CaitlynHeadshotCount), 0);
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.CaitlynPiltoverPeacemaker))
            {
                charVars.PercentOfAttack = 1;
                AddBuff((ObjAIBase)owner, owner, new Buffs.CantAttack(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.CaitlynHeadshotpassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}