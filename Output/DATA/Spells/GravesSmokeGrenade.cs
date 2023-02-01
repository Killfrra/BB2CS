#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GravesSmokeGrenade : BBBuffScript
    {
        Particle particle2;
        Particle particle;
        float lastTimeExecuted;
        float[] effect0 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        public override void OnActivate()
        {
            TeamId casterID;
            casterID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle2, out this.particle, "Graves_SmokeGrenade_Cloud_Team_Green.troy", "Graves_SmokeGrenade_Cloud_Team_Red.troy", casterID, 250, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            int level;
            float nextBuffVars_MovementSpeedMod;
            int nextBuffVars_SightReduction;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
                {
                    nextBuffVars_MovementSpeedMod = this.effect0[level];
                    AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeBoomSlow(nextBuffVars_MovementSpeedMod), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, true, false);
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.GravesSmokeGrenadeDelay)) == 0)
                    {
                        nextBuffVars_SightReduction = -800;
                        AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeBoom(nextBuffVars_SightReduction), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.NocturneParanoiaTarget)) > 0)
                    {
                        AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeNocturneUlt(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class GravesSmokeGrenade : BBSpellScript
    {
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            Minion other2;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(targetPos, ownerPos);
            FaceDirection(owner, targetPos);
            if(distance > 950)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            other2 = SpawnMinion("k", "TestCubeRender10Vision", "idle.lua", targetPos, teamID, true, true, false, true, true, true, 50, false, true, (Champion)attacker);
            SpellCast((ObjAIBase)owner, other2, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}