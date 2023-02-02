#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3027 : BBItemScript
    {
        float bonusHealth;
        float bonusAbilityPower;
        float bonusMana;
        float lastTimeExecuted;
        int ownerLevel;
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, this.bonusHealth);
            IncFlatMagicDamageMod(owner, this.bonusAbilityPower);
            IncFlatPARPoolMod(owner, this.bonusMana, PrimaryAbilityResourceType.MANA);
            SetSpellToolTipVar(this.bonusHealth, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            SetSpellToolTipVar(this.bonusAbilityPower, 3, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            SetSpellToolTipVar(this.bonusMana, 2, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnUpdateActions()
        {
            int tempLevel;
            if(ExecutePeriodically(60, ref this.lastTimeExecuted, false))
            {
                Particle thisParticle; // UNUSED
                this.bonusHealth += 18;
                this.bonusMana += 20;
                this.bonusAbilityPower += 2;
                this.bonusHealth = Math.Min(this.bonusHealth, 180);
                this.bonusMana = Math.Min(this.bonusMana, 200);
                this.bonusAbilityPower = Math.Min(this.bonusAbilityPower, 20);
                SpellEffectCreate(out thisParticle, out _, "RodofAges_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
            tempLevel = GetLevel(owner);
            if(tempLevel > this.ownerLevel)
            {
                AddBuff(attacker, target, new Buffs.CatalystHeal(), 1, 1, 8.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                this.ownerLevel = tempLevel;
            }
        }
        public override void OnActivate()
        {
            this.bonusHealth = 0;
            this.bonusMana = 0;
            this.bonusAbilityPower = 0;
            this.ownerLevel = GetLevel(owner);
        }
    }
}