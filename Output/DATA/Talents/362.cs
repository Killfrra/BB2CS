#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _362 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float hP;
            float plusHealth;
            hP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            plusHealth = hP * 0.03f;
            IncMaxHealth(owner, plusHealth, false);
        }
        public override void OnUpdateActions()
        {
            level = talentLevel;
            avatarVars.MasteryJuggernaut = true;
        }
    }
}