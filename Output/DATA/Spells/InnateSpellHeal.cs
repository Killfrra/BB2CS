#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class InnateSpellHeal : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 50f, 50f, 50f, 50f, 50f, },
            ChannelDuration = 13f,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void ChannelingStart()
        {
            float maxHP;
            float maxMP;
            float tickWorth;
            float tickWorthMana;
            float nextBuffVars_TickWorth;
            float nextBuffVars_TickWorthMana;
            maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            maxMP = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            tickWorth = maxHP / 21;
            tickWorthMana = maxMP / 6;
            nextBuffVars_TickWorth = tickWorth;
            nextBuffVars_TickWorthMana = tickWorthMana;
            AddBuff((ObjAIBase)owner, owner, new Buffs.InnateSpellHealCooldown(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.InnateSpellHeal(nextBuffVars_TickWorth, nextBuffVars_TickWorthMana), 1, 1, 13, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Meditate), (ObjAIBase)owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.InnateSpellHeal), (ObjAIBase)owner);
        }
    }
}
namespace Buffs
{
    public class InnateSpellHeal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Meditate",
            BuffTextureName = "MasterYi_Vanish.dds",
        };
        float tickWorth;
        float tickWorthMana;
        float tickNumber;
        bool willRemove;
        float lastTimeExecuted;
        public InnateSpellHeal(float tickWorth = default, float tickWorthMana = default)
        {
            this.tickWorth = tickWorth;
            this.tickWorthMana = tickWorthMana;
        }
        public override void OnActivate()
        {
            Particle arr; // UNUSED
            //RequireVar(this.tickWorth);
            //RequireVar(this.tickWorthMana);
            this.tickNumber = 1;
            SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, false))
            {
                if(!this.willRemove)
                {
                    float healAmount;
                    Particle arr; // UNUSED
                    float cD;
                    float newCD;
                    healAmount = this.tickWorth * this.tickNumber;
                    IncPAR(owner, this.tickWorthMana, PrimaryAbilityResourceType.MANA);
                    IncHealth(owner, healAmount, owner);
                    SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    this.tickNumber++;
                    cD = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    newCD = cD - 5;
                    SetSlotSpellCooldownTimeVer2(newCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                }
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource != default)
            {
                this.willRemove = true;
            }
        }
    }
}