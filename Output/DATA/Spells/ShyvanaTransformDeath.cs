#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaTransformDeath : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int casterID;
        public ShyvanaTransformDeath(int casterID = default)
        {
            this.casterID = casterID;
        }
        public override void OnDeactivate(bool expired)
        {
            PopCharacterData(owner, this.casterID);
        }
        public override void OnActivate()
        {
            //RequireVar(this.casterID);
        }
    }
}