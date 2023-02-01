#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerRevive : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {220, 240, 260, 280, 300, 320, 340, 360, 380, 400, 420, 440, 460, 480, 500, 520, 540, 560};
        int[] effect1 = {220, 240, 260, 280, 300, 320, 340, 360, 380, 400, 420, 440, 460, 480, 500, 520, 540, 560};
        public override bool CanCast()
        {
            bool returnValue = true;
            if(owner.IsDead)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void UpdateTooltip(int spellSlot)
        {
            float healthMod;
            float baseCooldown;
            float cooldownMultiplier;
            level = GetLevel(owner);
            healthMod = this.effect0[level];
            SetSpellToolTipVar(healthMod, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 540;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float cooldownMultiplier;
            float baseCooldown;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 540 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle ar; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_HealthMod;
            SpellEffectCreate(out ar, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
            ReincarnateHero(owner);
            if(avatarVars.DefensiveMastery == 1)
            {
                nextBuffVars_MoveSpeedMod = 1.25f;
                AddBuff((ObjAIBase)owner, target, new Buffs.SummonerReviveSpeedBoost(nextBuffVars_MoveSpeedMod), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
            }
            level = GetLevel(owner);
            nextBuffVars_HealthMod = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ReviveMarker(nextBuffVars_HealthMod), 1, 1, 120, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}