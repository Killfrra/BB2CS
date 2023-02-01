#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IndividualTurretAssistBonus : BBBuffScript
    {
        public override void OnActivate()
        {
            IncPermanentPercentAttackSpeedMod(attacker, 0.05f);
            IncPermanentFlatArmorMod(attacker, 5);
            IncPermanentFlatSpellBlockMod(attacker, 5);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentAttackSpeedMod(attacker, -0.05f);
            IncPermanentFlatArmorMod(attacker, -5);
            IncPermanentFlatSpellBlockMod(attacker, -5);
        }
    }
}