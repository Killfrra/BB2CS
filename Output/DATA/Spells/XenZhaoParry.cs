#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XenZhaoParry : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "XenZhaoParry",
            BuffTextureName = "XinZhao_CrescentSweep.dds",
        };
        float mRByLevel;
        Particle mRShield;
        float totalMR;
        public XenZhaoParry(float mRByLevel = default, float totalMR = default)
        {
            this.mRByLevel = mRByLevel;
            this.totalMR = totalMR;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            int level; // UNUSED
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.mRByLevel);
            IncFlatSpellBlockMod(owner, this.mRByLevel);
            SpellEffectCreate(out this.mRShield, out _, "xenZiou_SelfShield_01_magic.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.mRShield);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.totalMR);
        }
    }
}
namespace Spells
{
    public class XenZhaoParry : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {125, 225, 325};
        float[] effect1 = {0.15f, 0.15f, 0.15f};
        int[] effect2 = {25, 25, 25, 60, 70};
        int[] effect3 = {25, 25, 25, 60, 70};
        int[] effect4 = {7, 10, 13};
        int[] effect5 = {7, 10, 13};
        public override void SelfExecute()
        {
            float dtD;
            float percentByLevel;
            Particle p3; // UNUSED
            Vector3 castPos; // UNITIALIZED
            float weaponDmgBonus; // UNITIALIZED
            float dtDReal;
            TeamId teamID;
            float nextBuffVars_Count;
            float nextBuffVars_MRByLevel;
            float nextBuffVars_ScalingArmor;
            float nextBuffVars_ScalingMR;
            float nextBuffVars_CountMR;
            float nextBuffVars_TotalMR;
            float nextBuffVars_CountArmor;
            float nextBuffVars_TotalArmor;
            float currentHP;
            float percentDmg;
            bool isStealthed;
            Particle bye; // UNUSED
            Particle gda; // UNUSED
            Particle asdf; // UNUSED
            bool canSee;
            float armorAmount;
            dtD = this.effect0[level];
            percentByLevel = this.effect1[level];
            SpellEffectCreate(out p3, out _, "xenZiou_ult_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", castPos, owner, default, default, true, false, false, false, false);
            dtDReal = dtD + weaponDmgBonus;
            teamID = GetTeamID(owner);
            nextBuffVars_Count = 0;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                currentHP = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                percentDmg = currentHP * percentByLevel;
                dtDReal = dtD + percentDmg;
                isStealthed = GetStealthed(unit);
                SpellEffectCreate(out bye, out _, "xenZiou_utl_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                SpellEffectCreate(out gda, out _, "xenZiou_utl_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                SpellEffectCreate(out asdf, out _, "xenZiou_utl_tar_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                if(unit is not Champion)
                {
                    if(dtDReal > 600)
                    {
                        dtDReal = 600;
                    }
                }
                if(unit is Champion)
                {
                    nextBuffVars_Count++;
                }
                if(!isStealthed)
                {
                    ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
                else if(unit is Champion)
                {
                    ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
                else
                {
                    canSee = CanSeeTarget(owner, unit);
                    if(canSee)
                    {
                        ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                    }
                }
            }
            nextBuffVars_MRByLevel = this.effect2[level];
            armorAmount = this.effect3[level];
            nextBuffVars_ScalingArmor = this.effect4[level];
            nextBuffVars_ScalingMR = this.effect5[level];
            nextBuffVars_CountMR = nextBuffVars_Count * nextBuffVars_ScalingMR;
            nextBuffVars_TotalMR = nextBuffVars_CountMR + nextBuffVars_MRByLevel;
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoParry(nextBuffVars_MRByLevel, nextBuffVars_TotalMR), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_CountArmor = nextBuffVars_Count * nextBuffVars_ScalingArmor;
            nextBuffVars_TotalArmor = nextBuffVars_CountArmor + armorAmount;
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoSweepArmor(nextBuffVars_TotalArmor), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}