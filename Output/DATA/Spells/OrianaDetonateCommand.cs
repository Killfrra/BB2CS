#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaDetonateCommand : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Particle particle; // UNUSED
        int[] effect0 = {150, 225, 300};
        float[] effect1 = {-0.4f, -0.5f, -0.6f};
        int[] effect2 = {415, 415, 415, 415, 415};
        public override void SelfExecute()
        {
            float nextBuffVars_MoveSpeedMod; // UNUSED
            float damage;
            bool deployed;
            float rangeVar;
            float selfAP;
            float bonusDamage;
            TeamId teamID;
            Vector3 targetPos;
            int currentType;
            AttackableUnit other1;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            damage = this.effect0[level];
            deployed = false;
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            rangeVar = this.effect2[level];
            selfAP = GetFlatMagicDamageMod(owner);
            bonusDamage = selfAP * 0.7f;
            damage += bonusDamage;
            teamID = GetTeamID(owner);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetUnitPosition(unit);
                if(unit is Champion)
                {
                    currentType = 0;
                }
                else
                {
                    currentType = 1;
                }
                other1 = SetUnit(unit);
            }
            if(!deployed)
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                {
                    currentType = 5;
                    targetPos = charVars.BallPosition;
                }
                else
                {
                    targetPos = GetUnitPosition(owner);
                    currentType = 3;
                    targetPos = GetPointByUnitFacingOffset(owner, 0, 0);
                }
            }
            if(currentType != charVars.UltimateType)
            {
                currentType = 5;
                targetPos = charVars.UltimateTargetPos;
            }
            if(currentType == 0)
            {
                bool isStealthed;
                isStealthed = GetStealthed(other1);
                if(!isStealthed)
                {
                    SpellEffectCreate(out this.particle, out _, "Oriana_Shockwave_nova_ally.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out this.particle, out _, "Oriana_Shockwave_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
                }
            }
            else if(currentType == 1)
            {
                SpellEffectCreate(out this.particle, out _, "Oriana_Shockwave_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
            }
            else if(currentType == 2)
            {
                AttackableUnit unit; // UNITIALIZED
                SpellEffectCreate(out this.particle, out _, "Oriana_Shockwave_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
            }
            else if(currentType == 3)
            {
                SpellEffectCreate(out this.particle, out _, "Oriana_Shockwave_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
            }
            else if(currentType == 5)
            {
                SpellEffectCreate(out this.particle, out _, "Oriana_Shockwave_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, targetPos, rangeVar, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                bool canSee;
                bool validTarget;
                canSee = CanSeeTarget(owner, unit);
                validTarget = true;
                if(unit is not Champion)
                {
                    if(!canSee)
                    {
                        validTarget = false;
                    }
                }
                if(validTarget)
                {
                    Vector3 oldPos;
                    Particle temp; // UNUSED
                    Vector3 newPos;
                    float nextBuffVars_Distance;
                    int nextBuffVars_IdealDistance; // UNUSED
                    float nextBuffVars_Gravity;
                    float nextBuffVars_Speed;
                    Vector3 nextBuffVars_Center;
                    BreakSpellShields(unit);
                    oldPos = GetPointByUnitFacingOffset(unit, 425, 0);
                    FaceDirection(unit, targetPos);
                    SpellEffectCreate(out temp, out _, "OrianaDetonate_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, (ObjAIBase)owner);
                    newPos = GetPointByUnitFacingOffset(unit, 425, -180);
                    FaceDirection(unit, oldPos);
                    nextBuffVars_Distance = 790;
                    nextBuffVars_IdealDistance = 870;
                    nextBuffVars_Gravity = 25;
                    nextBuffVars_Speed = 775;
                    nextBuffVars_Center = newPos;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OrianaStun(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.MoveAwayCollision(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Center, nextBuffVars_Distance), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
            }
        }
    }
}