#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinHealthRelicAura : BBBuffScript
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
        bool killMe;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particleOrder, out _, "OdinHealthRelic.troy", default, TeamId.TEAM_NEUTRAL, 3000, 0, TeamId.TEAM_BLUE, default, owner, false, default, default, owner.Position, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.particleChaos, out _, "OdinHealthRelic.troy", default, TeamId.TEAM_NEUTRAL, 3000, 0, TeamId.TEAM_PURPLE, default, owner, false, default, default, owner.Position, owner, default, default, false, default, default, false);
            this.killMe = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleOrder);
            SpellEffectRemove(this.particleChaos);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            SetNoRender(owner, true);
        }
        public override void OnUpdateStats()
        {
            if(this.killMe)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinHealthRelicAura), (ObjAIBase)owner);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 150, SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    if(!this.killMe)
                    {
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinHealthRelicBuff(), 1, 1, 11, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        this.killMe = true;
                    }
                }
            }
        }
    }
}