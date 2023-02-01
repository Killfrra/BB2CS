#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingCloneSweep : BBBuffScript
    {
        public override void OnActivate()
        {
            int _1; // UNITIALIZED
            SpellCast((ObjAIBase)owner, owner, default, default, 2, SpellSlotType.SpellSlots, _1, false, false, false, true, false, false);
        }
    }
}