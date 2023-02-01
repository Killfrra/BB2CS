#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortuneRicochetShot : BBBuffScript
    {
    }
}
namespace Spells
{
    public class MissFortuneRicochetShot : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {25, 60, 95, 130, 165};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float attackDamage;
            float attackBonus;
            float abilityDamage;
            float damageToDeal;
            Particle asdf; // UNUSED
            Minion other1;
            Vector3 leftPos;
            Vector3 rightPos;
            Minion other2;
            Minion other3;
            Vector3 targetPos;
            int eatHydra;
            bool isStealthed;
            if(hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if(hitResult == HitResult.HIT_Dodge)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if(hitResult == HitResult.HIT_Miss)
            {
                hitResult = HitResult.HIT_Normal;
            }
            teamID = GetTeamID(attacker);
            attackDamage = GetTotalAttackDamage(attacker);
            attackBonus = 0.75f * attackDamage;
            abilityDamage = this.effect0[level];
            damageToDeal = attackBonus + abilityDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            SpellEffectCreate(out asdf, out _, "missFortune_richochet_tar_first.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            other1 = SpawnMinion("LocationFinder", "TestCube", "idle.lua", target.Position, teamID, true, true, true, true, true, true, 0, default, true);
            FaceDirection(other1, attacker.Position);
            leftPos = GetPointByUnitFacingOffset(other1, 500, 90);
            rightPos = GetPointByUnitFacingOffset(other1, 500, 270);
            other2 = SpawnMinion("LocationFinder", "TestCube", "idle.lua", leftPos, teamID, true, true, true, true, true, true, 0, default, true);
            other3 = SpawnMinion("LocationFinder", "TestCube", "idle.lua", rightPos, teamID, true, true, true, true, true, true, 0, default, true);
            FaceDirection(other2, attacker.Position);
            FaceDirection(other3, attacker.Position);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            targetPos = GetUnitPosition(other1);
            eatHydra = 0;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, other1.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(IsBehind(other1, unit))
                {
                    isStealthed = GetStealthed(unit);
                    if(!isStealthed)
                    {
                        AddBuff(attacker, unit, new Buffs.MissFortuneRShotHolder(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                }
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, other2.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.MissFortuneRShotHolder), true))
            {
                isStealthed = GetStealthed(unit);
                if(!isStealthed)
                {
                    SpellBuffRemove(unit, nameof(Buffs.MissFortuneRShotHolder), attacker);
                    AddBuff(attacker, unit, new Buffs.MissFortuneRicochetShot(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, other3.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.MissFortuneRShotHolder), true))
            {
                isStealthed = GetStealthed(unit);
                if(!isStealthed)
                {
                    SpellBuffRemove(unit, nameof(Buffs.MissFortuneRShotHolder), attacker);
                    AddBuff(attacker, unit, new Buffs.MissFortuneRicochetShot(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            foreach(AttackableUnit unit in GetRandomUnitsInArea(attacker, other1.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.MissFortuneRShotHolder), true))
            {
                SpellCast(attacker, unit, unit.Position, unit.Position, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, targetPos);
                eatHydra = 1;
            }
            if(eatHydra < 1)
            {
                foreach(AttackableUnit unit in GetRandomUnitsInArea(attacker, other1.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.MissFortuneRicochetShot), true))
                {
                    SpellCast(attacker, unit, unit.Position, unit.Position, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, targetPos);
                }
            }
        }
    }
}