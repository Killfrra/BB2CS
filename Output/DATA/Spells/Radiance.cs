#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Radiance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "BUFFBONE_CSTM_WEAPON_1", "BUFFBONE_CSTM_WEAPON_2", "spine", },
            AutoBuffActivateEffect = new[]{ "Taric_HammerFlare.troy", "Taric_HammerFlare.troy", "Taric_ShoulderFlare.troy", },
            BuffName = "Radiance",
            BuffTextureName = "GemKnight_Radiance.dds",
            NonDispellable = true,
            SpellToggleSlot = 4,
        };
        float damageIncrease;
        float abilityPower;
        Particle particle;
        float lastTimeExecuted;
        public Radiance(float damageIncrease = default, float abilityPower = default)
        {
            this.damageIncrease = damageIncrease;
            this.abilityPower = abilityPower;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            float damageIncrease;
            float nextBuffVars_DamageIncrease;
            float abilityPower;
            float nextBuffVars_AbilityPower;
            //RequireVar(this.damageIncrease);
            //RequireVar(this.abilityPower);
            IncFlatPhysicalDamageMod(owner, this.damageIncrease);
            IncFlatMagicDamageMod(owner, this.abilityPower);
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "taricgemstorm.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            damageIncrease = this.damageIncrease * 0.5f;
            nextBuffVars_DamageIncrease = damageIncrease;
            abilityPower = this.abilityPower * 0.5f;
            nextBuffVars_AbilityPower = abilityPower;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                AddBuff(attacker, unit, new Buffs.RadianceAura(nextBuffVars_DamageIncrease, nextBuffVars_AbilityPower), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                ApplyAssistMarker(attacker, unit, 10);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageIncrease);
            IncFlatMagicDamageMod(owner, this.abilityPower);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                float damageIncrease;
                float nextBuffVars_DamageIncrease;
                float abilityPower;
                float nextBuffVars_AbilityPower;
                damageIncrease = this.damageIncrease * 0.5f;
                nextBuffVars_DamageIncrease = damageIncrease;
                abilityPower = this.abilityPower * 0.5f;
                nextBuffVars_AbilityPower = abilityPower;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.RadianceAura(nextBuffVars_DamageIncrease, nextBuffVars_AbilityPower), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    ApplyAssistMarker(attacker, unit, 10);
                }
            }
        }
    }
}