#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VladimirTidesofBlood : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {30, 40, 50, 60, 70};
        public override void SelfExecute()
        {
            int count;
            float multiplier;
            float healthCost;
            float temp1;
            TeamId casterID; // UNUSED
            count = GetBuffCountFromAll(owner, nameof(Buffs.VladimirTidesofBloodCost));
            charVars.NumTideStacks = count;
            multiplier = count * 0.25f;
            multiplier++;
            healthCost = this.effect0[level];
            healthCost *= multiplier;
            temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
            casterID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 620, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                bool canSee;
                canSee = CanSeeTarget(owner, target);
                if(canSee)
                {
                    SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                }
            }
            AddBuff(attacker, attacker, new Buffs.VladimirTidesofBloodCost(), 4, 1, 10, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.VladimirTidesofBloodNuke(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}