#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptVladimir : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float maxHP;
            float baseHP;
            float healthPerLevel;
            float levelHealth;
            float totalBaseHealth;
            float totalBonusHealth;
            maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            baseHP = 400;
            healthPerLevel = 85;
            level = GetLevel(owner);
            levelHealth = level * healthPerLevel;
            totalBaseHealth = levelHealth + baseHP;
            totalBonusHealth = maxHP - totalBaseHealth;
            totalBonusHealth *= 0.15f;
            SetSpellToolTipVar(totalBonusHealth, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.4f, ref this.lastTimeExecuted, false))
            {
                if(owner.IsDead)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VladDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VladDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.VladDeathParticle), (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.VladimirBloodGorged(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}