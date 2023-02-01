#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ArchangelsStaff : BBBuffScript
    {
        float maxMana;
        public override void OnActivate()
        {
            //RequireVar(this.maxMana);
        }
        public override void OnUpdateStats()
        {
            float bonusAbilityPower;
            bonusAbilityPower = 0.025f * this.maxMana;
            IncFlatMagicDamageMod(owner, bonusAbilityPower);
        }
        public override void OnUpdateActions()
        {
            this.maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}