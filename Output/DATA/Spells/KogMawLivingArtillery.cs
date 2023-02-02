#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KogMawLivingArtillery : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 120, 160};
        int[] effect1 = {80, 120, 160};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            float aDRatio;
            float bonusDamage;
            Vector3 targetPos;
            Particle a; // UNUSED
            Minion other3;
            float damageAmount;
            float nextBuffVars_BaseDamageAmount;
            int count;
            float count2;
            float extraCost;
            float nextBuffVars_BonusDamage;
            float nextBuffVars_FinalDamage;
            Region nextBuffVars_Bubble; // UNUSED
            teamOfOwner = GetTeamID(owner);
            aDRatio = 0.5f;
            bonusDamage = GetFlatPhysicalDamageMod(owner);
            bonusDamage *= aDRatio;
            targetPos = GetCastSpellTargetPos();
            SpellEffectCreate(out a, out _, "KogMawLivingArtillery_mis.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "C_Mouth_d", default, attacker, default, default, true, default, default, false, false);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            damageAmount = this.effect0[level];
            nextBuffVars_BaseDamageAmount = this.effect1[level];
            nextBuffVars_BonusDamage = bonusDamage;
            damageAmount += bonusDamage;
            nextBuffVars_FinalDamage = damageAmount;
            AddBuff(attacker, other3, new Buffs.KogMawLivingArtillery(nextBuffVars_FinalDamage, nextBuffVars_BaseDamageAmount, nextBuffVars_BonusDamage), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            count = GetBuffCountFromAll(owner, nameof(Buffs.KogMawLivingArtilleryCost));
            count2 = 1 + count;
            extraCost = 40 * count2;
            extraCost = Math.Min(160, extraCost);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, extraCost, PrimaryAbilityResourceType.MANA);
            AddBuff(attacker, owner, new Buffs.KogMawLivingArtilleryCost(), 5, 1, 6, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_Bubble = AddPosPerceptionBubble(teamOfOwner, 100, targetPos, 1, default, false);
        }
    }
}
namespace Buffs
{
    public class KogMawLivingArtillery : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[]{ "NewYearDragonKogMaw", },
            SpellVOOverrideSkins = new[]{ "NewYearDragonKogMaw", },
        };
        float finalDamage;
        float baseDamageAmount;
        float bonusDamage;
        Particle particle1;
        Particle particle;
        Particle a; // UNUSED
        public KogMawLivingArtillery(float finalDamage = default, float baseDamageAmount = default, float bonusDamage = default)
        {
            this.finalDamage = finalDamage;
            this.baseDamageAmount = baseDamageAmount;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            int kMSkinID;
            //RequireVar(this.finalDamage);
            //RequireVar(this.baseDamageAmount);
            //RequireVar(this.bonusDamage);
            teamID = GetTeamID(attacker);
            kMSkinID = GetSkinID(attacker);
            if(kMSkinID == 5)
            {
                SpellEffectCreate(out this.particle1, out this.particle, "KogMawLivingArtillery_cas_chinese_green.troy", "KogMawLivingArtillery_cas_chinese_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle1, out this.particle, "KogMawLivingArtillery_cas_green.troy", "KogMawLivingArtillery_cas_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            int kMSkinID;
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle1);
            teamID = GetTeamID(owner);
            kMSkinID = GetSkinID(attacker);
            if(kMSkinID == 5)
            {
            }
            else
            {
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out this.a, out _, "KogMawLivingArtillery_tar_green.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, default, true, owner, default, default, target, default, default, true, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out this.a, out _, "KogMawLivingArtillery_tar_green.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, default, true, owner, default, default, target, default, default, true, default, default, false, false);
                }
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 240, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.KogMawLivingArtillerySight(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, unit, this.finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
            }
            this.baseDamageAmount *= 2.5f;
            this.finalDamage = this.baseDamageAmount + this.bonusDamage;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 240, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.KogMawLivingArtillerySight(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, unit, this.finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}