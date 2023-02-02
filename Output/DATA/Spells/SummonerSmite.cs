#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerSmite : BBSpellScript
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
            float bonusDamage;
            float totalDamage;
            float baseCooldown;
            ownerLevel = GetLevel(owner);
            bonusDamage = ownerLevel * 25;
            totalDamage = bonusDamage + 420;
            SetSpellToolTipVar(totalDamage, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 70;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier;
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier;
                float baseCooldown;
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 70 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle castParticle; // UNUSED
            int ownerLevel;
            float bonusDamage;
            float totalDamage;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            ownerLevel = GetLevel(owner);
            bonusDamage = ownerLevel * 25;
            totalDamage = bonusDamage + 420;
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            if(avatarVars.DefensiveMastery == 1)
            {
                IncGold(owner, 10);
            }
        }
    }
}