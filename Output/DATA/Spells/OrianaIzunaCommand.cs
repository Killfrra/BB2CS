#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaIzunaCommand : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {60, 100, 140, 180, 220};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float castRange;
            float distance;
            bool nextBuffVars_GhostAlive; // UNUSED
            bool deployed;
            bool shiftWithoutMissile;
            Vector3 castPos;
            Vector3 leftPoint;
            Vector3 rightPoint;
            float leftDistance;
            float rightDistance;
            Vector3 nextBuffVars_TargetPos; // UNUSED
            Vector3 nextBuffVars_CastPos; // UNUSED
            float nextBuffVars_TotalDamage;
            SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
            SetSpellOffsetTarget(3, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SpellBuffClear(owner, nameof(Buffs.OrianaBlendDelay));
            targetPos = GetCastSpellTargetPos();
            FaceDirection(owner, targetPos);
            ownerPos = GetUnitPosition(owner);
            charVars.IzunaPercent = 1;
            castRange = 885;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            if(distance > castRange)
            {
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
            }
            if(distance <= 150)
            {
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 150, 0);
            }
            nextBuffVars_GhostAlive = charVars.GhostAlive;
            deployed = false;
            shiftWithoutMissile = false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetCastSpellTargetPos();
                distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                if(distance > castRange)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
                }
                castPos = GetUnitPosition(unit);
                SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                distance = DistanceBetweenPoints(castPos, targetPos);
                if(distance >= 75)
                {
                    nextBuffVars_TargetPos = targetPos;
                    nextBuffVars_CastPos = castPos;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
                else
                {
                    shiftWithoutMissile = true;
                }
            }
            if(!deployed)
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                {
                    castPos = charVars.BallPosition;
                    SpellBuffClear(owner, nameof(Buffs.OriannaBallTracker));
                    targetPos = GetCastSpellTargetPos();
                    distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                    if(distance > castRange)
                    {
                        targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
                    }
                    distance = DistanceBetweenPoints(charVars.BallPosition, targetPos);
                    if(distance >= 75)
                    {
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_CastPos = charVars.BallPosition;
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, charVars.BallPosition);
                    }
                    else
                    {
                        castPos = charVars.BallPosition;
                        shiftWithoutMissile = true;
                    }
                }
                else
                {
                    castPos = GetUnitPosition(owner);
                    distance = DistanceBetweenPoints(castPos, targetPos);
                    if(distance >= 75)
                    {
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_CastPos = castPos;
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                    }
                    else
                    {
                        shiftWithoutMissile = true;
                    }
                }
            }
            if(shiftWithoutMissile)
            {
                TeamId teamID;
                Minion other3;
                Particle temp; // UNUSED
                teamID = GetTeamID(owner);
                other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", targetPos, teamID ?? TeamId.TEAM_BLUE, false, true, false, true, true, true, 0, default, true, (Champion)owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectCreate(out temp, out _, "Oriana_Izuna_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, default, default, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, other3.Position, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    float baseDamage;
                    float aP;
                    float bonusDamage;
                    float totalDamage;
                    baseDamage = this.effect0[level];
                    aP = GetFlatMagicDamageMod(owner);
                    bonusDamage = aP * 0.6f;
                    totalDamage = bonusDamage + baseDamage;
                    totalDamage *= charVars.IzunaPercent;
                    charVars.IzunaPercent *= 0.9f;
                    charVars.IzunaPercent = Math.Max(0.4f, charVars.IzunaPercent);
                    nextBuffVars_TotalDamage = totalDamage;
                    if(GetBuffCountFromCaster(unit, default, nameof(Buffs.OrianaIzunaDamage)) == 0)
                    {
                        BreakSpellShields(unit);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            leftPoint = GetPointByUnitFacingOffset(owner, 500, 90);
            rightPoint = GetPointByUnitFacingOffset(owner, 500, -90);
            leftDistance = DistanceBetweenPoints(castPos, leftPoint);
            rightDistance = DistanceBetweenPoints(castPos, rightPoint);
            if(leftDistance >= rightDistance)
            {
                PlayAnimation("Spell1b", 1, owner, true, false, false);
            }
            else
            {
                PlayAnimation("Spell1", 1, owner, true, false, false);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}