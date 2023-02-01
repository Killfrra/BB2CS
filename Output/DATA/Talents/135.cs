#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _135 : BBCharScript
    {
        public override void OnUpdateActions()
        {
            if(talentLevel == 2)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Monsterbuffs2(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MonsterBuffs(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}