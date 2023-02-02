#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KarmaSoulShield : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240, 280};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_TotalArmorAmount;
            float abilityPower;
            float armorAmount;
            float totalArmorAmount;
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaSoulShieldAnim(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            abilityPower = GetFlatMagicDamageMod(attacker);
            armorAmount = this.effect0[level];
            abilityPower *= 0.8f;
            totalArmorAmount = abilityPower + armorAmount;
            nextBuffVars_TotalArmorAmount = totalArmorAmount;
            AddBuff(attacker, target, new Buffs.KarmaSoulShield(nextBuffVars_TotalArmorAmount), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class KarmaSoulShield : BBBuffScript
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
        public KarmaSoulShield(float totalArmorAmount = default)
        {
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            //RequireVar(this.totalArmorAmount);
            SpellEffectCreate(out this.particle, out _, "karma_soulShield_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out this.soundParticle, out _, "KarmaSoulShieldSound.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
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