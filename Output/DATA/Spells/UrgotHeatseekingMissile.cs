#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UrgotHeatseekingMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            int homed;
            float distance;
            TeamId teamID;
            float distanceObjs;
            Particle hit; // UNUSED
            targetPos = GetCastSpellTargetPos();
            FaceDirection(owner, targetPos);
            ownerPos = GetUnitPosition(owner);
            homed = 0;
            distance = DistanceBetweenPoints(targetPos, ownerPos);
            teamID = GetTeamID(owner);
            if(distance <= 3000)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, 999, nameof(Buffs.UrgotCorrosiveDebuff), true))
                {
                    if(homed == 0)
                    {
                        distanceObjs = DistanceBetweenObjects("Owner", "Unit");
                        if(distanceObjs <= 1200)
                        {
                            SpellEffectCreate(out hit, out _, "UrgotHeatseekingIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, default, default, targetPos, default, default, targetPos, true);
                            homed = 1;
                            SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
                            SpellEffectCreate(out hit, out _, "UrgotTargetIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                        }
                    }
                }
                if(homed == 0)
                {
                    foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 999, nameof(Buffs.UrgotCorrosiveDebuff), true))
                    {
                        if(homed == 0)
                        {
                            distanceObjs = DistanceBetweenObjects("Owner", "Unit");
                            if(distanceObjs <= 1200)
                            {
                                SpellEffectCreate(out hit, out _, "UrgotHeatseekingIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, default, default, targetPos, default, default, targetPos, true);
                                homed = 1;
                                SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
                                SpellEffectCreate(out hit, out _, "UrgotTargetIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                            }
                        }
                    }
                }
            }
            if(homed == 0)
            {
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false, owner.Position);
            }
        }
    }
}