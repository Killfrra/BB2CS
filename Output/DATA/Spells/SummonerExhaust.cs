#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerExhaust : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown;
            baseCooldown = 210;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier;
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier;
                float baseCooldown;
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 210 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle castParticle; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            nextBuffVars_MoveSpeedMod = -0.4f;
            AddBuff(attacker, target, new Buffs.ExhaustSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.SummonerExhaust(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            if(avatarVars.OffensiveMastery == 1)
            {
                float nextBuffVars_ArmorMod;
                nextBuffVars_ArmorMod = -10;
                AddBuff(attacker, target, new Buffs.ExhaustDebuff(nextBuffVars_ArmorMod), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
            }
        }
    }
}
namespace Buffs
{
    public class SummonerExhaust : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, null, "", },
            AutoBuffActivateEffect = new[]{ "summoner_banish.troy", "", "", },
            BuffName = "ExhaustDebuff",
            BuffTextureName = "Summoner_Exhaust.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource == default)
            {
                damageAmount *= 0.3f;
            }
            else if(damageSource != default)
            {
                if(damageType != DamageType.DAMAGE_TYPE_TRUE)
                {
                    damageAmount *= 0.65f;
                }
            }
        }
    }
}