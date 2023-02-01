#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShrineHealAura : BBBuffScript
    {
        Particle particleOrder;
        Particle particleChaos;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particleOrder, out _, "odin_shrine_heal.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_BLUE, default, owner, false, default, default, owner.Position, owner, default, default, false);
            SpellEffectCreate(out this.particleChaos, out _, "odin_shrine_heal.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_PURPLE, default, owner, false, default, default, owner.Position, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleOrder);
            SpellEffectRemove(this.particleChaos);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectHeroes, default, true))
                {
                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinShrineHealBuff)) == 0)
                    {
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinShrineHealBuff(), 1, 1, 13, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}