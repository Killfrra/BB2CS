#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MaokaiTrunkLine : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Particle partname; // UNUSED
        int[] effect0 = {70, 115, 160, 205, 250};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float baseDamage;
            float nextBuffVars_MoveSpeedMod;
            TeamId teamOfOwner;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 650)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.partname, out _, "maoki_trunkSmash_cas.troy", default, teamOfOwner ?? TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellBuffRemove(unit, nameof(Buffs.MaokaiTrunkLine), (ObjAIBase)owner);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.MaokaiTrunkLine)) == 0)
                {
                    bool isStealthed;
                    Particle a; // UNUSED
                    isStealthed = GetStealthed(unit);
                    if(!isStealthed)
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        SpellEffectCreate(out a, out _, "PowerballHit.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                        ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                        AddBuff(attacker, unit, new Buffs.MaokaiTrunkLineStun(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        if(unit is Champion)
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(unit);
                            SpellEffectCreate(out a, out _, "PowerballHit.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                            ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                            AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                            AddBuff(attacker, unit, new Buffs.MaokaiTrunkLineStun(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        }
                        else
                        {
                            bool canSee;
                            canSee = CanSeeTarget(owner, unit);
                            if(canSee)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(unit);
                                SpellEffectCreate(out a, out _, "PowerballHit.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                                ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                                AddBuff(attacker, unit, new Buffs.MaokaiTrunkLineStun(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class MaokaiTrunkLine : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "MaokaiTrunkSmash",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
    }
}