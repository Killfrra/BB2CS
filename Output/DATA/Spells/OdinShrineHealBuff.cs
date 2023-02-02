#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShrineHealBuff : BBBuffScript
    {
        bool willRemove;
        float tickWorth;
        float tickWorthMana;
        float tickNumber;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float maxHP;
            float maxMP;
            float tickWorth;
            float tickWorthMana;
            Particle arr; // UNUSED
            maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            maxMP = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            tickWorth = maxHP / 21;
            tickWorthMana = maxMP / 6;
            this.willRemove = false;
            this.tickWorth = tickWorth;
            this.tickWorthMana = tickWorthMana;
            this.tickNumber = 1;
            SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource != default)
            {
                this.willRemove = true;
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, false))
            {
                if(!this.willRemove)
                {
                    float healAmount;
                    Particle arr; // UNUSED
                    healAmount = this.tickWorth * this.tickNumber;
                    IncPAR(owner, this.tickWorthMana, PrimaryAbilityResourceType.MANA);
                    IncHealth(owner, healAmount, owner);
                    SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    this.tickNumber++;
                }
            }
        }
    }
}