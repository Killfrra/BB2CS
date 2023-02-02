#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HungeringStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.08f, 0.11f, 0.14f, 0.17f, 0.2f};
        int[] effect1 = {75, 125, 175, 225, 275};
        int[] effect2 = {75, 125, 175, 225, 275};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int warwickSkinID;
            Particle a; // UNUSED
            float nextBuffVars_DrainPercent;
            Particle ar; // UNUSED
            bool nextBuffVars_DrainedBool;
            warwickSkinID = GetSkinID(attacker);
            if(warwickSkinID == 6)
            {
                SpellEffectCreate(out a, out _, "HungeringStrikeFire_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "HungeringStrike_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            nextBuffVars_DrainPercent = 0.8f;
            nextBuffVars_DrainedBool = false;
            AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(target is Champion)
            {
                float temp1;
                float maxHealth;
                float percentDamage;
                float minDamage;
                float damageToDeal;
                temp1 = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                maxHealth = this.effect0[level];
                percentDamage = temp1 * maxHealth;
                minDamage = this.effect1[level];
                damageToDeal = Math.Max(minDamage, percentDamage);
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1, 1, false, false, attacker);
                SpellEffectCreate(out ar, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
            }
            else
            {
                ApplyDamage(attacker, target, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1, 1, false, false, attacker);
                SpellEffectCreate(out ar, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
            }
        }
    }
}