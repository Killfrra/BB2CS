#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptWarwick : BBCharScript
    {
        int[] effect0 = {6, 6, 6, 6, 6, 6, 12, 12, 12, 12, 12, 12, 18, 18, 18, 18, 18, 18};
        public override void SetVarsByLevel()
        {
            charVars.LifeStealAmount = this.effect0[level];
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        if(target is BaseTurret)
                        {
                        }
                        else
                        {
                            int count;
                            float lifeStealToHeal;
                            Particle num; // UNUSED
                            AddBuff(attacker, target, new Buffs.EternalThirst(), 3, 1, 4.1f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            count = GetBuffCountFromAll(target, nameof(Buffs.EternalThirst));
                            lifeStealToHeal = charVars.LifeStealAmount * count;
                            IncHealth(owner, lifeStealToHeal, owner);
                            SpellEffectCreate(out num, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, target, new Buffs.EternalThirstIcon(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 2)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.BloodScent_internal(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}