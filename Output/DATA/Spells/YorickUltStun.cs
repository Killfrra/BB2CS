#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickUltStun : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetStunned(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
        }
    }
}