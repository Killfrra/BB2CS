#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerRally : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
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
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle castParticle; // UNUSED
            Vector3 minionPos;
            Particle ba; // UNUSED
            TeamId ownerID;
            float duration;
            Minion other3;
            int ownerLevel;
            float bonusHealth;
            float bonusRegen;
            float nextBuffVars_FinalHPRegen; // UNUSED
            float nextBuffVars_BonusHealth;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            minionPos = GetPointByUnitFacingOffset(owner, 200, 0);
            SpellEffectCreate(out ba, out _, "summoner_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, minionPos, target, default, default, false, default, default, false);
            ownerID = GetTeamID(owner);
            duration = 15;
            if(avatarVars.RallyDurationBonus == 5)
            {
                duration += avatarVars.RallyDurationBonus;
            }
            else if(avatarVars.RallyDurationBonus == 10)
            {
                duration += avatarVars.RallyDurationBonus;
            }
            other3 = SpawnMinion("Beacon", "SummonerBeacon", "idle.lua", minionPos, ownerID ?? TeamId.TEAM_BLUE, true, true, false, false, true, false, 0, true, false);
            ownerLevel = GetLevel(owner);
            bonusHealth = ownerLevel * 25;
            bonusRegen = ownerLevel * 1.5f;
            nextBuffVars_FinalHPRegen = bonusRegen + 15;
            nextBuffVars_BonusHealth = bonusHealth;
            if(avatarVars.RallyAPMod == 70)
            {
                AddBuff((ObjAIBase)owner, other3, new Buffs.BeaconAuraAP(nextBuffVars_BonusHealth), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                AddBuff((ObjAIBase)owner, other3, new Buffs.BeaconAura(nextBuffVars_BonusHealth), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}