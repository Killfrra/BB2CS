#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenTriCleaveUnlock : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.UnlockAnimation));
        }
        public override void OnUpdateActions()
        {
            bool temp;
            temp = IsMoving(owner);
            if(temp)
            {
                SpellBuffClear(owner, nameof(Buffs.RivenTriCleaveUnlock));
            }
        }
    }
}