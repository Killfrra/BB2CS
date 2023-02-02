#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OlafAxeThrowDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        int[] effect0 = {50, 90, 130, 170, 210};
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            int level;
            float bonusDamage;
            float totalDamage;
            bool isStealthed;
            float damageToDeal;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out a, out _, "olaf_axeThrow_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out _, "olaf_axeThrow_tar_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out _, "olaf_axeThrow_tar_03.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = this.effect0[level];
            totalDamage = GetTotalAttackDamage(attacker);
            isStealthed = GetStealthed(owner);
            totalDamage *= 0.5f;
            damageToDeal = bonusDamage + totalDamage;
            if(!isStealthed)
            {
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else if(owner is Champion)
            {
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else
            {
                bool canSee;
                canSee = CanSeeTarget(attacker, owner);
                if(canSee)
                {
                    ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
            }
        }
    }
}