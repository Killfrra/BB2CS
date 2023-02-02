#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class DeathsCaress : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathsCaress)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.DeathsCaress), (ObjAIBase)owner);
            }
            else
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class DeathsCaress : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "DeathsCaress_buf.troy", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "Death's Caress",
            BuffTextureName = "Sion_DeathsCaress.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float totalArmorAmount;
        float finalArmorAmount;
        float lastTimeExecuted;
        float ticktimer;
        float oldArmorAmount;
        int[] effect0 = {-70, -80, -90, -100, -110};
        public DeathsCaress(float totalArmorAmount = default, float finalArmorAmount = default, float ticktimer = default)
        {
            this.totalArmorAmount = totalArmorAmount;
            this.finalArmorAmount = finalArmorAmount;
            this.ticktimer = ticktimer;
        }
        public override void OnActivate()
        {
            int level;
            float manaCostInc;
            //RequireVar(this.totalArmorAmount);
            //RequireVar(this.finalArmorAmount);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DeathsCaress));
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 4);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            manaCostInc = this.effect0[level];
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, manaCostInc, PrimaryAbilityResourceType.MANA);
            IncreaseShield(owner, this.totalArmorAmount, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            if(this.totalArmorAmount > 0)
            {
                Particle varrr; // UNUSED
                RemoveShield(owner, this.totalArmorAmount, true, true);
                SpellEffectCreate(out varrr, out _, "DeathsCaress_nova.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 525, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, this.finalArmorAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DeathsCaressFull));
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = 8 * multiplier;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            SetBuffToolTipVar(1, this.totalArmorAmount);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                this.ticktimer--;
                if(this.ticktimer < 4)
                {
                    Say(owner, " ", this.ticktimer);
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.totalArmorAmount;
            if(this.totalArmorAmount >= damageAmount)
            {
                this.totalArmorAmount -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.totalArmorAmount;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.totalArmorAmount;
                this.totalArmorAmount = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}