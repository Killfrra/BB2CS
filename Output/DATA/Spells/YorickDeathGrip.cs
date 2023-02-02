#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickDeathGrip : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {19, 19, 19, 19, 19};
        int[] effect1 = {900, 900, 900, 900, 900};
        int[] effect2 = {200, 300, 400, 400, 400};
        float[] effect3 = {0.5f, 0.5f, 0.5f};
        int[] effect4 = {5, 7, 9};
        int[] effect5 = {5, 7, 9};
        int[] effect6 = {5, 7, 9};
        float[] effect7 = {0.5f, 0.5f, 0.5f};
        int[] effect8 = {5, 7, 9};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            TeamId teamID;
            int iterations;
            float lineWidth;
            bool foundFirstPos;
            int nextBuffVars_DamageToDeal;
            Vector3 firstPos;
            Vector3 lastPos;
            Minion other1;
            Minion other2;
            Minion other3;
            Vector3 nextBuffVars_Pos;
            int nextBuffVars_DurationLevel;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            teamID = GetTeamID(owner);
            iterations = this.effect0[level];
            lineWidth = this.effect1[level];
            foundFirstPos = false;
            nextBuffVars_DamageToDeal = this.effect2[level];
            foreach(Vector3 pos in GetPointsOnLine(ownerPos, targetPos, lineWidth, distance, iterations))
            {
                nextBuffVars_Pos = pos;
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickDeathGripDelay(nextBuffVars_DamageToDeal, nextBuffVars_Pos), 50, 1, this.effect3[level], BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                if(!foundFirstPos)
                {
                    firstPos = pos;
                    foundFirstPos = true;
                }
                lastPos = pos;
            }
            other1 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", firstPos, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, default, true, (Champion)owner);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, this.effect4[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            other2 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", lastPos, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300, default, true, (Champion)owner);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, this.effect5[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            nextBuffVars_DurationLevel = this.effect6[level];
            AddBuff(other1, other2, new Buffs.YorickDeathGripBeamDelay(nextBuffVars_DurationLevel), 1, 1, this.effect7[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other2);
            other3 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", targetPos, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 300 + lineWidth, default, true, (Champion)owner);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, this.effect8[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other3);
            LinkVisibility(other2, other3);
        }
    }
}
namespace Buffs
{
    public class YorickDeathGrip : BBBuffScript
    {
        float damageToDeal;
        Vector3 pos;
        float lastTimeExecuted;
        public YorickDeathGrip(float damageToDeal = default, Vector3 pos = default)
        {
            this.damageToDeal = damageToDeal;
            this.pos = pos;
        }
        public override void OnActivate()
        {
            Vector3 pos;
            //RequireVar(this.damageToDeal);
            //RequireVar(this.pos);
            pos = this.pos;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, default, nameof(Buffs.YorickDeathGripExtra)) > 0)
                {
                }
                else
                {
                    AddBuff(attacker, unit, new Buffs.YorickDeathGripExtra(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.YorickDeathGripTarget(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SNARE, 0, true, false, false);
                }
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.1f, ref this.lastTimeExecuted, false))
            {
                Vector3 pos;
                pos = this.pos;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(GetBuffCountFromCaster(unit, default, nameof(Buffs.YorickDeathGripExtra)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.YorickDeathGripExtra(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.YorickDeathGripTarget(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SNARE, 0, true, false, false);
                    }
                }
            }
        }
    }
}