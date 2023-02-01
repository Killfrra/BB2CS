#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Crowstorm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Crowstorm",
            BuffTextureName = "Fiddlesticks_Crowstorm.dds",
            SpellFXOverrideSkins = new[]{ "SurprisePartyFiddlesticks", },
        };
        float damageAmount;
        Particle particle;
        Particle particle2;
        float lastTimeExecuted;
        public Crowstorm(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.damageAmount);
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out this.particle2, "Crowstorm_green_cas.troy", "Crowstorm_red_cas.troy", teamOfOwner, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 600, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage((ObjAIBase)owner, unit, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.225f, 1, false, false, attacker);
                }
            }
        }
    }
}
namespace Spells
{
    public class Crowstorm : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 150f, 120f, 100f, },
            ChannelDuration = 1.5f,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle confetti;
        float[] effect0 = {62.5f, 112.5f, 162.5f};
        public override void ChannelingStart()
        {
            Vector3 castPos;
            TeamId teamID; // UNUSED
            int fiddlesticksSkinID;
            castPos = GetCastSpellTargetPos();
            FaceDirection(owner, castPos);
            teamID = GetTeamID(attacker);
            fiddlesticksSkinID = GetSkinID(attacker);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectCreate(out this.confetti, out _, "Party_HornConfetti.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "BUFFBONE_CSTM_HORN", default, attacker, default, default, false, false, false, false, false);
            }
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID;
            Vector3 castPos;
            Particle p3; // UNUSED
            Particle ar; // UNUSED
            Particle ar1; // UNUSED
            float nextBuffVars_DamageAmount;
            int fiddlesticksSkinID;
            teamID = GetTeamID(owner);
            castPos = GetCastSpellTargetPos();
            SpellEffectCreate(out p3, out _, "summoner_flashback.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out ar, out _, "summoner_cast.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out ar1, out _, "summoner_flash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, castPos, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.ParanoiaMissChance(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            TeleportToPosition(owner, castPos);
            nextBuffVars_DamageAmount = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.Crowstorm(nextBuffVars_DamageAmount), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            teamID = GetTeamID(attacker);
            fiddlesticksSkinID = GetSkinID(attacker);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(this.confetti);
            }
        }
        public override void ChannelingCancelStop()
        {
            TeamId teamID; // UNUSED
            int fiddlesticksSkinID;
            teamID = GetTeamID(attacker);
            fiddlesticksSkinID = GetSkinID(attacker);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(this.confetti);
            }
        }
    }
}