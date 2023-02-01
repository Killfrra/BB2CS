#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _413 : BBCharScript
    {
        int[] effect0 = {4, 8, 12, 16};
        int[] effect1 = {4, 7, 10, 10};
        public override void OnUpdateStats()
        {
            float manaMod;
            float energy;
            int charLevel;
            float mana;
            level = talentLevel;
            manaMod = this.effect0[level];
            energy = this.effect1[level];
            charLevel = GetLevel(owner);
            mana = manaMod * charLevel;
            IncFlatPARPoolMod(owner, mana, PrimaryAbilityResourceType.MANA);
            IncFlatPARPoolMod(owner, energy, PrimaryAbilityResourceType.Energy);
        }
    }
}