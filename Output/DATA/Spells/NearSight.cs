#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NearSight : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NearSight",
            BuffTextureName = "Nocturne_Paranoia.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetNearSight(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNearSight(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetNearSight(owner, true);
        }
    }
}