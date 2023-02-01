#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _127 : BBCharScript
    {
        public override void OnDodge()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Nimbleness(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0);
        }
    }
}