#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OlafRecklessStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Particle particleID; // UNUSED
        int[] effect0 = {40, 64, 88, 112, 136};
        int[] effect1 = {100, 160, 220, 280, 340};
        public override bool CanCast()
        {
            bool returnValue = true;
            int selfDamage;
            float currentHealth;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            selfDamage = this.effect0[level];
            currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(currentHealth <= selfDamage)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float bonusDamage;
            float selfDamage;
            Particle b; // UNUSED
            Particle a; // UNUSED
            Particle c; // UNUSED
            bonusDamage = this.effect1[level];
            selfDamage = bonusDamage * 0.4f;
            SpellEffectCreate(out b, out _, "olaf_recklessSwing_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out this.particleID, out _, "olaf_recklessStrike_axe_charge.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_WEAPON_L_2", default, owner, "BUFFBONE_WEAPON_L_4", default, false);
            SpellEffectCreate(out this.particleID, out _, "olaf_recklessStrike_axe_charge.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_WEAPON_R_2", default, owner, "BUFFBONE_WEAPON_R_4", default, false);
            SpellEffectCreate(out a, out _, "olaf_recklessSwing_tar_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out c, out _, "olaf_recklessSwing_tar_05.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out c, out _, "olaf_recklessSwing_tar_03.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            ApplyDamage(attacker, attacker, selfDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
        }
    }
}