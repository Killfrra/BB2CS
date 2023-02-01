#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KarmaSoulShieldC : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.troy",
            BuffName = "KarmaSoulShield",
            BuffTextureName = "KarmaSoulShield.dds",
            OnPreDamagePriority = 3,
        };
        float totalArmorAmount;
        Particle particle;
        Particle soundParticle; // UNUSED
        float oldArmorAmount;
        public KarmaSoulShieldC(float totalArmorAmount = default)
        {
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            //RequireVar(this.totalArmorAmount);
            SpellEffectCreate(out this.particle, out _, "karma_soulShield_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.soundParticle, out _, "KarmaSoulShieldSound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false);
            SetBuffToolTipVar(1, this.totalArmorAmount);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, this.totalArmorAmount, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            if(this.totalArmorAmount > 0)
            {
                RemoveShield(owner, this.totalArmorAmount, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.totalArmorAmount;
            if(this.totalArmorAmount >= damageAmount)
            {
                this.totalArmorAmount -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.totalArmorAmount;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.totalArmorAmount;
                this.totalArmorAmount = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
namespace Spells
{
    public class KarmaSoulShieldC : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240, 280};
        int[] effect1 = {0, 0, 0, 0, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float nextBuffVars_TotalArmorAmount;
            float abilityPower;
            float armorAmount;
            float totalArmorAmount;
            Particle a; // UNUSED
            Particle aoehit; // UNUSED
            teamID = GetTeamID(owner);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaSoulShieldAnim(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            abilityPower = GetFlatMagicDamageMod(attacker);
            armorAmount = this.effect0[level];
            abilityPower *= 0.8f;
            totalArmorAmount = abilityPower + armorAmount;
            nextBuffVars_TotalArmorAmount = totalArmorAmount;
            AddBuff(attacker, target, new Buffs.KarmaSoulShieldC(nextBuffVars_TotalArmorAmount), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out a, out _, "karma_soulShield_buf_mantra.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out aoehit, out _, "karma_souldShiled_ult_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false);
                ApplyDamage(attacker, unit, armorAmount + this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 0, false, false, attacker);
            }
        }
    }
}