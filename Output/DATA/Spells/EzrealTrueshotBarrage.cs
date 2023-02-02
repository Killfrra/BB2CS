#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class EzrealTrueshotBarrage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {0, 0, 0, 0, 0};
        int[] effect1 = {350, 500, 650};
        public override void UpdateTooltip(int spellSlot)
        {
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float spell3Display;
            totalDamage = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            bonusDamage = totalDamage - baseDamage;
            spell3Display = bonusDamage * 1;
            SetSpellToolTipVar(spell3Display, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void SelfExecute()
        {
            int ownerSkinID;
            TeamId ownerTeamID;
            ownerSkinID = GetSkinID(owner);
            ownerTeamID = GetTeamID(owner);
            if(ownerSkinID == 5)
            {
                Particle asdf; // UNUSED
                SpellEffectCreate(out asdf, out _, "Ezreal_PulseFire_Ult_Thrusters.troy", default, ownerTeamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_Vent_Low_R", default, owner, default, default, true, false, false, false, false);
                SpellEffectCreate(out asdf, out _, "Ezreal_PulseFire_Ult_Thrusters.troy", default, ownerTeamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_Vent_Low_L", default, owner, default, default, true, false, false, false, false);
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float percentOfAttack;
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            Particle asdf; // UNUSED
            float physPreMod;
            float physPostMod; // UNUSED
            float aPPreMod;
            float aPPostMod; // UNUSED
            teamID = GetTeamID(attacker);
            percentOfAttack = charVars.PercentOfAttack;
            totalDamage = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            bonusDamage = totalDamage - baseDamage;
            bonusDamage *= 1;
            SpellEffectCreate(out asdf, out _, "Ezreal_TrueShot_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
            AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + this.effect0[level], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            physPreMod = GetFlatPhysicalDamageMod(owner);
            physPostMod = 1 * physPreMod;
            aPPreMod = GetFlatMagicDamageMod(owner);
            aPPostMod = 0.9f * aPPreMod;
            ApplyDamage((ObjAIBase)owner, target, bonusDamage + this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0.9f, 1, false, false, attacker);
            charVars.PercentOfAttack *= 0.92f;
            charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.3f);
        }
    }
}