#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CatalystHeal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CatalystHeal",
            BuffTextureName = "3010_Catalyst_the_Protector.dds",
        };
        Particle cp1;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.cp1, out _, "env_manaheal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.cp1);
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                IncHealth(owner, 15.625f, owner);
                IncPAR(owner, 12.5f, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}