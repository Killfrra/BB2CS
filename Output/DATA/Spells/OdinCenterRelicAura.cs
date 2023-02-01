#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCenterRelicAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle buffParticle;
        bool killMe;
        Particle particle; // UNUSED
        Particle particle2; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetForceRenderParticles(owner, true);
            SetNoRender(owner, true);
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_blue_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            this.killMe = false;
            SpellEffectCreate(out this.particle, out _, "PotionofGiantStrength_itm.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false, false, default, false, false);
            SpellEffectCreate(out this.particle, out _, "PlaceholderShield.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false, false, default, false, false);
            SpellEffectCreate(out this.particle2, out _, "PotionofElusiveness_itm.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, target, default, default, false, false, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            SetNoRender(owner, true);
        }
        public override void OnUpdateStats()
        {
            if(this.killMe)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinCenterRelicAura), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            float newDuration;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 175, SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    if(!this.killMe)
                    {
                        teamID = GetTeamID(unit);
                        if(teamID == TeamId.TEAM_BLUE)
                        {
                            newDuration = 60;
                            if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                            {
                                newDuration *= 1.15f;
                            }
                            else
                            {
                                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.Monsterbuffs2)) > 0)
                                {
                                    newDuration *= 1.3f;
                                }
                            }
                            AddBuff((ObjAIBase)unit, unit, new Buffs.OdinCenterRelicBuff(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                            this.killMe = true;
                            AddBuff((ObjAIBase)unit, unit, new Buffs.OdinScoreBigRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}