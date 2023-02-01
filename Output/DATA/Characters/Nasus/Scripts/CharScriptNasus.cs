#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptNasus : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float damageBonus;
            level = GetLevel(owner);
            if(level >= 11)
            {
                IncPercentLifeStealMod(owner, 0.2f);
            }
            else if(level >= 6)
            {
                IncPercentLifeStealMod(owner, 0.17f);
            }
            else
            {
                IncPercentLifeStealMod(owner, 0.14f);
            }
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                damageBonus = 0 + charVars.DamageBonus;
                SetSpellToolTipVar(damageBonus, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            Particle num; // UNUSED
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    SpellEffectCreate(out num, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.SoulEater(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.DamageBonus = 0;
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}