#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AnnieTibbersBasicAttack : BBSpellScript
    {
        int[] effect0 = {80, 105, 130, 130, 130};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float dmg;
            teamID = GetTeamID(owner);
            attacker = GetChampionBySkinName("Annie", teamID ?? TeamId.TEAM_UNKNOWN);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            dmg = this.effect0[level];
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, default, false, false);
        }
    }
}