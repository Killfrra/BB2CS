#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VladimirSanguinePool : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "VladimirSanguinePool",
            BuffTextureName = "Vladimir_SanguinePool.dds",
            IsDeathRecapSource = true,
            SpellFXOverrideSkins = new[]{ "BloodkingVladimir", },
            SpellVOOverrideSkins = new[]{ "BloodkingVladimir", },
        };
        float damageTick;
        float moveSpeedMod;
        Fade iD; // UNUSED
        Particle particle;
        float hasteBoost;
        Particle particle1; // UNUSED
        float lastTimeExecuted2;
        float damagePulse;
        float slowPulse;
        public VladimirSanguinePool(float damageTick = default, float moveSpeedMod = default)
        {
            this.damageTick = damageTick;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.damageTick);
            //RequireVar(this.moveSpeedMod);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetCanAttack(owner, false);
            SetSilenced(owner, true);
            SetForceRenderParticles(owner, true);
            SetCallForHelpSuppresser(owner, true);
            this.iD = PushCharacterFade(owner, 0, 0.1f);
            teamOfOwner = GetTeamID(attacker);
            SpellEffectCreate(out this.particle, out _, "VladSanguinePool_buf.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 2.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Idle1down", 2.25f, owner, false, true, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            this.hasteBoost = 0.375f;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamOfOwner; // UNITIALIZED
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            SetSilenced(owner, false);
            SetForceRenderParticles(owner, false);
            SetCallForHelpSuppresser(owner, false);
            this.iD = PushCharacterFade(owner, 1, 0.1f);
            SpellEffectRemove(this.particle);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
            }
            SpellEffectCreate(out this.particle1, out _, "Vlad_Bloodking_Blood_Skin.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted2, false))
            {
                this.hasteBoost = 0;
            }
            IncFlatAttackRangeMod(owner, -450);
            IncPercentMovementSpeedMod(owner, this.hasteBoost);
            SetGhosted(owner, true);
            SetForceRenderParticles(owner, true);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_MoveSpeedMod;
            float healAmount;
            float duration;
            int skinID;
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            if(ExecutePeriodically(0.5f, ref this.damagePulse, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, this.damageTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    healAmount = 0.15f * this.damageTick;
                    IncHealth(owner, healAmount, owner);
                }
            }
            if(ExecutePeriodically(0.25f, ref this.slowPulse, true))
            {
                duration = GetBuffRemainingDuration(owner, nameof(Buffs.VladimirSanguinePool));
                skinID = GetSkinID(owner);
                if(skinID == 5)
                {
                    if(duration <= 1)
                    {
                        AddBuff(attacker, target, new Buffs.VladimirSanguinePoolParticle(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class VladimirSanguinePool : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {-0.4f, -0.4f, -0.4f, -0.4f, -0.4f};
        float[] effect1 = {20, 33.75f, 47.5f, 61.25f, 75};
        public override void SelfExecute()
        {
            float currentHealth;
            float healthCost;
            Particle hi; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_DamageTick;
            float damageTick;
            float maxHP;
            float baseHP;
            float healthPerLevel;
            float levelHealth;
            float totalBaseHealth;
            float totalBonusHealth;
            float healthMod;
            DestroyMissileForTarget(owner);
            currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            healthCost = currentHealth * -0.2f;
            IncHealth(owner, healthCost, owner);
            SpellEffectCreate(out hi, out _, "Vlad_Bloodking_Blood_Skin.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, false, false, false, false);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            damageTick = this.effect1[level];
            maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            baseHP = 400;
            healthPerLevel = 85;
            level = GetLevel(owner);
            levelHealth = level * healthPerLevel;
            totalBaseHealth = levelHealth + baseHP;
            totalBonusHealth = maxHP - totalBaseHealth;
            healthMod = totalBonusHealth * 0.0375f;
            nextBuffVars_DamageTick = healthMod + damageTick;
            AddBuff(attacker, attacker, new Buffs.VladimirSanguinePool(nextBuffVars_DamageTick, nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}