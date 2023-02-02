#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RenektonReignOfTheTyrant : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {300, 450, 600};
        int[] effect1 = {20, 35, 50};
        float[] effect2 = {0.75f, 1, 1.25f};
        public override void SelfExecute()
        {
            int nextBuffVars_Level;
            float baseBurn;
            float selfAP;
            float aPBonus;
            float nextBuffVars_BonusHealth;
            float nextBuffVars_MaximumSpeed; // UNUSED
            float nextBuffVars_BurnDamage;
            nextBuffVars_Level = level;
            nextBuffVars_BonusHealth = this.effect0[level];
            baseBurn = this.effect1[level];
            nextBuffVars_MaximumSpeed = this.effect2[level];
            selfAP = GetFlatMagicDamageMod(owner);
            aPBonus = 0.05f * selfAP;
            nextBuffVars_BurnDamage = baseBurn + aPBonus;
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonReignOfTheTyrant(nextBuffVars_Level, nextBuffVars_BonusHealth, nextBuffVars_BurnDamage), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RenektonReignOfTheTyrant : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", "", "", },
            AutoBuffActivateEffect = new[]{ "RenektonDominus_sword.troy", "RenektonDominus_aura.troy", "", "", },
            BuffName = "RenekthonTyrantForm",
            BuffTextureName = "Renekton_Dominus.dds",
            NonDispellable = true,
        };
        int level;
        float bonusHealth;
        float burnDamage;
        int bonusSpeed; // UNUSED
        float lastTimeExecuted;
        float[] effect0 = {2.5f, 2.5f, 2.5f};
        float[] effect1 = {1.25f, 1.25f, 1.25f};
        public RenektonReignOfTheTyrant(int level = default, float bonusHealth = default, float burnDamage = default)
        {
            this.level = level;
            this.bonusHealth = bonusHealth;
            this.burnDamage = burnDamage;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "RenektonDominus_transform", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            IncScaleSkinCoef(0.2f, owner);
            //RequireVar(this.level);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.burnDamage);
            //RequireVar(this.maximumSpeed);
            this.bonusSpeed = 0;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage((ObjAIBase)owner, unit, this.burnDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out _, out _, "RenektonDominus_transform", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            int level; // UNUSED
            level = this.level;
            IncMaxHealth(owner, this.bonusHealth, true);
            IncScaleSkinCoef(0.2f, owner);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                int level;
                float healthPercent;
                level = this.level;
                healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.Other);
                IncPAR(owner, this.effect0[level], PrimaryAbilityResourceType.Other);
                if(healthPercent <= charVars.RageThreshold)
                {
                    IncPAR(owner, this.effect1[level], PrimaryAbilityResourceType.Other);
                }
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage((ObjAIBase)owner, unit, this.burnDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
                }
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, default, true))
                {
                    ApplyDamage((ObjAIBase)owner, unit, this.burnDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}