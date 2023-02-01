#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShrineAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShamanAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle particleOrder;
        Particle particleChaos;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particleOrder, out _, "odin_shrine_aura.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_BLUE, default, owner, false, default, default, owner.Position, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.particleChaos, out _, "odin_shrine_aura.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_PURPLE, default, owner, false, default, default, owner.Position, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleOrder);
            SpellEffectRemove(this.particleChaos);
        }
        public override void OnUpdateActions()
        {
            float _0_5; // UNITIALIZED
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, false, _0_5))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OdinShrineBuff(), 1, 1, 45, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}