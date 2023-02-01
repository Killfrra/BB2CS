#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinSigilAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle particleOrder;
        Particle particleChaos;
        Particle buffParticle;
        bool killMe;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particleOrder, out _, "OdinSigil.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_BLUE, default, owner, false, default, default, owner.Position, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.particleChaos, out _, "OdinSigil.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_PURPLE, default, owner, false, default, default, owner.Position, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            this.killMe = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleOrder);
            SpellEffectRemove(this.particleChaos);
            SpellEffectRemove(this.buffParticle);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            SetNoRender(owner, true);
        }
        public override void OnUpdateStats()
        {
            if(this.killMe)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinSigilAura), (ObjAIBase)owner);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 175, SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    if(!this.killMe)
                    {
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinSigilBuff(), 1, 1, 40, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        this.killMe = true;
                    }
                }
            }
        }
    }
}