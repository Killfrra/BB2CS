#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PersonalTurretAssistBonus : BBBuffScript
    {
        public override void OnActivate()
        {
            IncPermanentPercentAttackSpeedMod(attacker, 0.3f);
            IncPermanentFlatArmorMod(attacker, 25);
            IncPermanentFlatSpellBlockMod(attacker, 25);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentAttackSpeedMod(attacker, -0.2f);
            IncPermanentFlatArmorMod(attacker, -15);
            IncPermanentFlatSpellBlockMod(attacker, -15);
        }
    }
}