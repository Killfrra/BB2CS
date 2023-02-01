#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretBackdoorBonus : BBBuffScript
    {
        public override void OnActivate()
        {
            IncPermanentFlatArmorMod(owner, -150);
            IncPermanentFlatSpellBlockMod(owner, -150);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatArmorMod(owner, 150);
            IncPermanentFlatSpellBlockMod(owner, 150);
        }
    }
}