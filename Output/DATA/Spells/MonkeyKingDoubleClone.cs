#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingDoubleClone : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            SetNoRender(owner, true);
            SetCanMove(owner, false);
        }
    }
}