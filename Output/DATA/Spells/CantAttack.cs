#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CantAttack : BBBuffScript
    {
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
        }
    }
}