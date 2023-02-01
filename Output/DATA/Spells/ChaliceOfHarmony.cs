#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ChaliceOfHarmony : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            float percentMana;
            float percentMissing;
            percentMana = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            percentMissing = 1 - percentMana;
            IncPercentPARRegenMod(owner, percentMissing, PrimaryAbilityResourceType.MANA);
        }
    }
}