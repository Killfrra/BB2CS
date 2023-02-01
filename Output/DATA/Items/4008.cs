#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class _4008 : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            float healthPercent;
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(healthPercent <= 0.5f)
            {
                IncFlatHPRegenMod(owner, 1);
                IncFlatArmorMod(owner, 10);
            }
        }
    }
}