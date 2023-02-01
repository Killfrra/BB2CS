#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _434 : BBCharScript
    {
        public override void OnUpdateActions()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonsterBuffs(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}