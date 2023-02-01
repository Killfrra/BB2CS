#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MuramasaCheck : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnKill()
        {
            if(target is Champion)
            {
                AddBuff(attacker, attacker, new Buffs.MuramasaStats(), 20, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0);
                AddBuff(attacker, attacker, new Buffs.MuramasaStats(), 20, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0);
            }
        }
        public override void OnAssist()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MuramasaStats(), 20, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0);
        }
    }
}