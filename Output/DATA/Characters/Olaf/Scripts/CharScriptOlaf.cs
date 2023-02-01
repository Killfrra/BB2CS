#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOlaf : BBCharScript
    {
        float lastTimeExecuted;
        float bonusDamage;
        public override void OnUpdateActions()
        {
            float maxHealth;
            float healthDamage;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                healthDamage = maxHealth * 0.01f;
                SetSpellToolTipVar(healthDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentHealth;
            if(attacker == owner)
            {
                currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if(currentHealth <= damageAmount)
                {
                    damageAmount = currentHealth - 1;
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            Vector3 targetPos;
            float distance;
            Vector3 facingPos;
            Vector3 nextBuffVars_FacingPos;
            Vector3 nextBuffVars_TargetPos;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.OlafAxeThrow))
            {
                targetPos = GetCastSpellTargetPos();
                distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                distance += 50;
                facingPos = GetPointByUnitFacingOffset(owner, distance, 0);
                nextBuffVars_FacingPos = facingPos;
                nextBuffVars_TargetPos = targetPos;
                AddBuff((ObjAIBase)owner, owner, new Buffs.OlafAxeThrow(nextBuffVars_FacingPos, nextBuffVars_TargetPos), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            float maxHealth;
            float healthDamage;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OlafBerzerkerRage(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            healthDamage = maxHealth * 0.004f;
            this.bonusDamage = 12 + healthDamage;
            SetSpellToolTipVar(this.bonusDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 3)
            {
                IncPermanentFlatArmorPenetrationMod(owner, 10);
            }
        }
    }
}