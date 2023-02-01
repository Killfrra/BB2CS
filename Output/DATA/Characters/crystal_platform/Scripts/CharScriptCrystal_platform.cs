#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptCrystal_platform : BBCharScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetGhosted(owner, true);
            SetCanMove(owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinDisintegrate(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}