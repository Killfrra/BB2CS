#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerHeal : BBSpellScript
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
            float bonusHeal;
            float totalHeal;
            float secondaryHeal;
            float baseCooldown;
            ownerLevel = GetLevel(owner);
            bonusHeal = ownerLevel * 25;
            totalHeal = bonusHeal + 140;
            if(avatarVars.DefensiveMastery == 1)
            {
                totalHeal *= 1.1f;
            }
            secondaryHeal = totalHeal * 0.5f;
            SetSpellToolTipVar(totalHeal, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            SetSpellToolTipVar(secondaryHeal, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 270;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier;
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 3, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier;
                float baseCooldown;
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 270 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void SelfExecute()
        {
            Particle ar; // UNUSED
            SpellEffectCreate(out ar, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int ownerLevel;
            float bonusHeal;
            float totalHeal;
            float secondaryHeal;
            ownerLevel = GetLevel(owner);
            bonusHeal = ownerLevel * 25;
            totalHeal = bonusHeal + 140;
            if(avatarVars.DefensiveMastery == 1)
            {
                totalHeal *= 1.1f;
            }
            secondaryHeal = totalHeal * 0.5f;
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.SummonerHealCheck)) > 0)
            {
                if(target == owner)
                {
                    IncHealth(target, totalHeal, owner);
                }
                else
                {
                    secondaryHeal *= 0.5f;
                    IncHealth(target, secondaryHeal, owner);
                    ApplyAssistMarker(attacker, target, 10);
                }
            }
            else
            {
                if(target == owner)
                {
                    IncHealth(target, totalHeal, owner);
                }
                else
                {
                    IncHealth(target, secondaryHeal, owner);
                    ApplyAssistMarker(attacker, target, 10);
                }
            }
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.SummonerHealCheck)) == 0)
            {
                AddBuff((ObjAIBase)target, target, new Buffs.SummonerHealCheck(), 1, 1, 25, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}