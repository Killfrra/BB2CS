#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliShadowDanceTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
        };
        public override void OnDeactivate(bool expired)
        {
            if(!owner.IsDead)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
            }
        }
    }
}