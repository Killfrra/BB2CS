#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NoRespawn : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NoRespawn",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            IncPercentRespawnTimeMod(owner, -3000);
        }
        public override void OnDeactivate(bool expired)
        {
            Alert("Should not be here");
        }
        public override void OnUpdateStats()
        {
            IncPercentRespawnTimeMod(owner, -3000);
        }
        public override void OnDeath()
        {
            float var;
            var = GetPercentRespawnTimeMod(owner);
            Alert("YO!", var);
        }
    }
}