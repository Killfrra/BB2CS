#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneTumbleFailsafe : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneTumble)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneTumbleBonus)) == 0)
                {
                    UnlockAnimation(owner, true);
                    SetCanAttack(owner, true);
                    SetCanMove(owner, true);
                }
            }
        }
    }
}