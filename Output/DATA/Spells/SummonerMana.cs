#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerMana : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            int ownerLevel;
            float bonusMana;
            float totalMana;
            float secondaryMana;
            float baseCooldown;
            float cooldownMultiplier;
            ownerLevel = GetLevel(owner);
            bonusMana = ownerLevel * 30;
            totalMana = bonusMana + 160;
            if(avatarVars.UtilityMastery == 1)
            {
                totalMana *= 1.2f;
            }
            secondaryMana = totalMana * 0.5f;
            SetSpellToolTipVar(totalMana, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            SetSpellToolTipVar(secondaryMana, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 180;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 3, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float cooldownMultiplier;
            float baseCooldown;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 180 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void SelfExecute()
        {
            Particle castParticle; // UNUSED
            int ownerLevel;
            float bonusMana;
            float totalMana;
            float secondaryMana;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            ownerLevel = GetLevel(owner);
            bonusMana = ownerLevel * 30;
            totalMana = bonusMana + 160;
            if(avatarVars.UtilityMastery == 1)
            {
                totalMana *= 1.2f;
            }
            secondaryMana = totalMana * 0.5f;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 600, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out castParticle, out _, "Summoner_Mana.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, false, false, false, false);
                if(unit == owner)
                {
                    IncPAR(unit, totalMana, PrimaryAbilityResourceType.MANA);
                }
                else
                {
                    IncPAR(unit, secondaryMana, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}