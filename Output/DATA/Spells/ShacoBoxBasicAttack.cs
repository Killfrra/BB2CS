#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShacoBoxBasicAttack : BBSpellScript
    {
        int[] effect0 = {35, 55, 75, 95, 115};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            float dmg;
            teamID = GetTeamID(owner);
            attacker = GetChampionBySkinName("Shaco", TeamId.TEAM_BLUE);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            dmg = this.effect0[level];
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.25f, 1, false, false, (ObjAIBase)owner);
        }
    }
}