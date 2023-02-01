#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OriannaBallTracker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShadowWalk",
            BuffTextureName = "Blitzcrank_StaticField.dds",
        };
        Vector3 myPosition;
        public OriannaBallTracker(Vector3 myPosition = default)
        {
            this.myPosition = myPosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.myPosition);
            charVars.BallPosition = this.myPosition;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(attacker, nameof(Buffs.OrianaGhost));
        }
    }
}