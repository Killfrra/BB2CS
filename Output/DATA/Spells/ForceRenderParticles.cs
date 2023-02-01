#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ForceRenderParticles : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForceRenderParticles",
        };
        public override void OnActivate()
        {
            SetForceRenderParticles(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetForceRenderParticles(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetForceRenderParticles(owner, true);
        }
    }
}