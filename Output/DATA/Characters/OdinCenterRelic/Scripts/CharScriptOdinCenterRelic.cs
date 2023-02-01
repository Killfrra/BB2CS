#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdinCenterRelic : BBCharScript
    {
        public override void OnActivate()
        {
            SetMagicImmune(owner, true);
            SetPhysicalImmune(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinBombBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}