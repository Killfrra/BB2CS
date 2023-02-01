#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerClairvoyance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GuinsoosRodofOblivion_buf.troy", },
        };
        Particle particleID;
        Particle particleID2;
        Region bubble;
        public SummonerClairvoyance(Particle particleID = default, Particle particleID2 = default, Region bubble = default)
        {
            this.particleID = particleID;
            this.particleID2 = particleID2;
            this.bubble = bubble;
        }
        public override void OnActivate()
        {
            //RequireVar(this.particleID);
            //RequireVar(this.particleID2);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubble);
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.particleID2);
        }
    }
}
namespace Spells
{
    public class SummonerClairvoyance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown;
            int summonerCooldownBonus; // UNITIALIZED
            float cooldownMultiplier;
            float duration;
            baseCooldown = 70;
            if(summonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            duration = 4;
            if(avatarVars.UtilityMastery == 1)
            {
                duration += 2;
            }
            SetSpellToolTipVar(duration, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            int summonerCooldownBonus; // UNITIALIZED
            float cooldownMultiplier;
            float baseCooldown;
            if(summonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 70 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Particle castParticle; // UNUSED
            TeamId teamID;
            Particle particleID;
            Particle particleID2;
            Particle nextBuffVars_ParticleID;
            Particle nextBuffVars_ParticleID2;
            Region nextBuffVars_Bubble;
            float duration;
            targetPos = GetCastSpellTargetPos();
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out particleID, out particleID2, "ClairvoyanceEyeLong_green.troy", "ClairvoyanceEyeLong_red.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            nextBuffVars_ParticleID = particleID;
            nextBuffVars_ParticleID2 = particleID2;
            duration = 4;
            if(avatarVars.UtilityMastery == 1)
            {
                duration += 2;
            }
            nextBuffVars_Bubble = AddPosPerceptionBubble(teamID, 1400, targetPos, duration, default, false);
            AddBuff(attacker, owner, new Buffs.SummonerClairvoyance(nextBuffVars_ParticleID, nextBuffVars_ParticleID2, nextBuffVars_Bubble), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}