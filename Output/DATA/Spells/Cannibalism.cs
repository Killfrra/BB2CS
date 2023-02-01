#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Cannibalism : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Cannibalism_buf.troy", },
            BuffTextureName = "Sion_Cannibalism.dds",
        };
        float healPercent;
        float lifestealPercent;
        float attackSpeedMod;
        public Cannibalism(float healPercent = default, float lifestealPercent = default, float attackSpeedMod = default)
        {
            this.healPercent = healPercent;
            this.lifestealPercent = lifestealPercent;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healPercent);
            //RequireVar(this.lifestealPercent);
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, this.lifestealPercent);
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            Particle particle; // UNUSED
            float healAmount;
            float temp1;
            Particle particle2; // UNUSED
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(damageSource == default)
                    {
                        if(target.Team != owner.Team)
                        {
                            SpellEffectCreate(out particle, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                            healAmount = damageAmount * this.healPercent;
                            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                            {
                                temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                                if(temp1 < 1)
                                {
                                    IncHealth(unit, healAmount, owner);
                                    ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                                }
                                SpellEffectCreate(out particle2, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, false, false, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class Cannibalism : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.5f, 0.75f, 1};
        float[] effect1 = {0.25f, 0.375f, 0.5f};
        float[] effect2 = {0.5f, 0.5f, 0.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_LifestealPercent;
            float nextBuffVars_HealPercent;
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_LifestealPercent = this.effect0[level];
            nextBuffVars_HealPercent = this.effect1[level];
            nextBuffVars_AttackSpeedMod = this.effect2[level];
            AddBuff(attacker, target, new Buffs.Cannibalism(nextBuffVars_HealPercent, nextBuffVars_LifestealPercent, nextBuffVars_AttackSpeedMod), 1, 1, 20, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}