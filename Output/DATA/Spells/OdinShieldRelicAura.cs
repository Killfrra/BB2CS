#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShieldRelicAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle buffParticle;
        bool killMe;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetForceRenderParticles(owner, true);
            SetNoRender(owner, true);
            SpellEffectCreate(out this.buffParticle, out _, "odin_heal_rune.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, true, default, false, false);
            this.killMe = false;
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
                SpellBuffRemove(owner, nameof(Buffs.OdinShieldRelicAura), (ObjAIBase)owner, 0);
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
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinShieldRelicBuffHeal(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinScoreSmallRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        this.killMe = true;
                    }
                }
            }
        }
    }
}