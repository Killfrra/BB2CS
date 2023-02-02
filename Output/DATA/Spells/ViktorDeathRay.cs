#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ViktorDeathRay : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {70, 115, 160, 205, 250};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPosStart;
            Vector3 targetPosEnd;
            Minion other1;
            TeamId teamID;
            float baseDamage;
            float aPVAL;
            float aPBONUS;
            float totalDamage;
            float damageForDot;
            PlayAnimation("Spell3", 0, owner, false, false, false);
            teamOfOwner = GetTeamID(attacker);
            targetPosStart = GetCastSpellTargetPos();
            targetPosEnd = GetCastSpellDragEndPos();
            other1 = SpawnMinion("MaokaiSproutling", "MaokaiSproutling", "idle.lua", targetPosStart, teamOfOwner ?? TeamId.TEAM_CASTER, false, false, false, false, true, true, 0, false, true, (Champion)owner);
            AddBuff(attacker, other1, new Buffs.ViktorExpirationTimer(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            FaceDirection(other1, targetPosEnd);
            targetPosEnd = GetPointByUnitFacingOffset(other1, 700, 0);
            teamID = GetTeamID(owner);
            AddBuff(attacker, other1, new Buffs.ViktorDeathRay(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Move(other1, targetPosEnd, 550, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 500, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            baseDamage = this.effect0[level];
            aPVAL = GetFlatMagicDamageMod(owner);
            aPBONUS = aPVAL * 0.7f;
            totalDamage = aPBONUS + baseDamage;
            damageForDot = totalDamage * 0.075f;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, targetPosStart, 140, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.ViktorDeathRayBuff)) == 0)
                {
                    Particle a; // UNUSED
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    SpellEffectCreate(out a, out _, "ViktorEntropicBeam_hit.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.ViktorDeathRayBuff(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentE)) > 0)
                    {
                        float nextBuffVars_DamageForDot;
                        nextBuffVars_DamageForDot = damageForDot;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.ViktorDeathRayDOT(nextBuffVars_DamageForDot), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class ViktorDeathRay : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "GalioRighteousGust",
            BuffTextureName = "",
        };
        Particle particleID;
        Particle a;
        Particle hit;
        float lastTimeExecuted;
        int[] effect0 = {70, 115, 160, 205, 250};
        public override void OnActivate()
        {
            Vector3 laserPos; // UNITIALIZED
            SpellEffectCreate(out this.particleID, out _, "ViktorEntropicBeam_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "Up_Hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.a, out _, "ViktorEntropicBeam_tar_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.hit, out _, "ViktorEntropicBeam_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, default, default, laserPos, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 0, false, false, attacker);
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.hit);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster;
            TeamId ownerTeam;
            Vector3 laserPos;
            int level;
            float baseDamage;
            float aPVAL;
            float aPBONUS;
            float totalDamage;
            float damageForDot;
            caster = SetBuffCasterUnit();
            ownerTeam = GetTeamID(owner);
            laserPos = GetUnitPosition(owner);
            level = GetSlotSpellLevel(caster, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            aPVAL = GetFlatMagicDamageMod(caster);
            aPBONUS = aPVAL * 0.7f;
            totalDamage = aPBONUS + baseDamage;
            damageForDot = totalDamage * 0.075f;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, laserPos, 140, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(GetBuffCountFromCaster(unit, caster, nameof(Buffs.ViktorDeathRayBuff)) == 0)
                    {
                        Particle a; // UNUSED
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                        SpellEffectCreate(out a, out _, "ViktorEntropicBeam_hit.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        AddBuff(caster, unit, new Buffs.ViktorDeathRayBuff(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if(GetBuffCountFromCaster(caster, caster, nameof(Buffs.ViktorAugmentE)) > 0)
                        {
                            float nextBuffVars_DamageForDot;
                            nextBuffVars_DamageForDot = damageForDot;
                            AddBuff(caster, unit, new Buffs.ViktorDeathRayDOT(nextBuffVars_DamageForDot), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}