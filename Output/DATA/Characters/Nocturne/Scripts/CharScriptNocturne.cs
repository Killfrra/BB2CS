#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptNocturne : BBCharScript
    {
        float[] effect0 = {0.2f, 0.05f, 0.05f, 0.05f, 0.05f};
        public override void OnUpdateActions()
        {
            float bonusAD;
            float bonusAD2;
            float curTime;
            float timeSinceLastHit;
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD2 = bonusAD * 1.2f;
            bonusAD *= 0.75f;
            SetSpellToolTipVar(bonusAD, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            SetSpellToolTipVar(bonusAD2, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            if(!owner.IsDead)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) == 0)
                {
                    curTime = GetGameTime();
                    timeSinceLastHit = curTime - charVars.LastHit;
                    if(timeSinceLastHit >= 10)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.NocturneUmbraBlades(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            Particle hi; // UNUSED
            float curTime;
            float timeSinceLastHit;
            if(hitResult != HitResult.HIT_Miss)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) > 0)
                    {
                        charVars.LastHit = GetGameTime();
                        if(target is ObjAIBase)
                        {
                            SpellEffectCreate(out hi, out _, "Globalhit_red.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, owner.Position, owner, default, default, true, default, default, false);
                        }
                    }
                    else
                    {
                        charVars.LastHit--;
                        curTime = GetGameTime();
                        timeSinceLastHit = curTime - charVars.LastHit;
                        if(timeSinceLastHit >= 9)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) == 0)
                            {
                                AddBuff((ObjAIBase)owner, owner, new Buffs.NocturneUmbraBlades(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string slotName;
            float cooldown;
            slotName = GetSpellName();
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(slotName == nameof(Spells.NocturneParanoia))
            {
                SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.NocturneParanoia2));
                SetSlotSpellCooldownTimeVer2(cooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.LastHit = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            float attackSpeedBoost;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackSpeedBoost = this.effect0[level];
            if(slot == 1)
            {
                IncPermanentPercentAttackSpeedMod(owner, attackSpeedBoost);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}