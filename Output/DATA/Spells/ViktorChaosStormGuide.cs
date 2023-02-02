#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ViktorChaosStormGuide : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void SelfExecute()
        {
            float hMCSStartTime;
            float hMCSCurrTime;
            float remainingBuffTime;
            bool hasTarget;
            Vector3 centerPos;
            Vector3 targetPos;
            hMCSStartTime = GetBuffStartTime(owner, nameof(Buffs.ViktorChaosStormGuide));
            hMCSCurrTime = GetTime();
            remainingBuffTime = hMCSCurrTime * hMCSStartTime;
            hasTarget = false;
            centerPos = GetUnitPosition(owner);
            targetPos = GetCastSpellTargetPos();
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, centerPos, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormGuide), true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.ViktorChaosStormGuide)) > 0)
                {
                    if(unit is Champion)
                    {
                        SpellBuffRemove(unit, nameof(Buffs.ViktorChaosStormGuide), (ObjAIBase)owner, 0);
                    }
                    else
                    {
                        SetInvulnerable(unit, false);
                        ApplyDamage((ObjAIBase)unit, unit, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, (ObjAIBase)unit);
                    }
                }
            }
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                AddBuff(attacker, unit, new Buffs.ViktorChaosStormGuide(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                hasTarget = true;
            }
            if(!hasTarget)
            {
                TeamId teamID;
                Minion other2;
                teamID = GetTeamID(owner);
                other2 = SpawnMinion("GuideMarker", "TestCube", default, targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, true, false, true, false, true, 0, false, false);
                AddBuff(attacker, other2, new Buffs.ViktorExpirationTimer(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff(attacker, other2, new Buffs.ViktorChaosStormGuide(), 1, 1, 7 + remainingBuffTime, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
            }
        }
    }
}
namespace Buffs
{
    public class ViktorChaosStormGuide : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "ViktorStormGuide",
            BuffTextureName = "ViktorChaosStorm.dds",
            IsPetDurationBuff = true,
        };
        Particle particle1;
        Particle particle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            SpellEffectCreate(out this.particle1, out _, "Viktor_ChaosStorm_indicator.troy", default, TeamId.TEAM_CASTER, 0, 0, TeamId.TEAM_CASTER, default, caster, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle2, out _, "Viktor_ChaosStorm_indicator_02.troy", default, TeamId.TEAM_CASTER, 0, 0, TeamId.TEAM_CASTER, default, caster, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                Vector3 ownerPos;
                ownerPos = GetUnitPosition(owner);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, ownerPos, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormAOE), true))
                {
                    float distance; // UNUSED
                    distance = DistanceBetweenObjects("Unit", "Owner");
                    if(caster.Team == unit.Team)
                    {
                        if(owner is Champion)
                        {
                            IssueOrder(unit, OrderType.MoveTo, default, owner);
                        }
                        else
                        {
                            IssueOrder(unit, OrderType.MoveTo, default, owner);
                        }
                    }
                }
            }
        }
    }
}