#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WallOfPain : BBBuffScript
    {
        float moveSpeedMod;
        float armorMod;
        Vector3 pos;
        float lastTimeExecuted;
        public WallOfPain(float moveSpeedMod = default, float armorMod = default, Vector3 pos = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.armorMod = armorMod;
            this.pos = pos;
        }
        public override void OnActivate()
        {
            Vector3 pos;
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_ArmorMod;
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.armorMod);
            //RequireVar(this.pos);
            pos = this.pos;
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            nextBuffVars_ArmorMod = this.armorMod;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.WallofPainTarget(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.WallofPainExtra(nextBuffVars_ArmorMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.WallOfPainMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            Vector3 pos;
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_ArmorMod;
            if(ExecutePeriodically(0.1f, ref this.lastTimeExecuted, false))
            {
                pos = this.pos;
                nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                nextBuffVars_ArmorMod = this.armorMod;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.WallofPainTarget), false))
                {
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.WallOfPainMarker)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.WallofPainTarget(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.WallofPainExtra(nextBuffVars_ArmorMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.WallOfPainMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class WallOfPain : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {17, 19, 21, 23, 25};
        int[] effect1 = {800, 900, 1000, 1100, 1200};
        float[] effect2 = {-0.4f, -0.5f, -0.6f, -0.7f, -0.8f};
        int[] effect3 = {-15, -20, -25, -30, -35};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            TeamId teamID;
            int iterations;
            float lineWidth;
            bool foundFirstPos;
            float nextBuffVars_MoveSpeedMod;
            int nextBuffVars_ArmorMod;
            Vector3 nextBuffVars_Pos;
            Vector3 firstPos;
            Vector3 lastPos;
            Minion other1;
            Minion other2;
            Minion other3;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            teamID = GetTeamID(owner);
            iterations = this.effect0[level];
            lineWidth = this.effect1[level];
            foundFirstPos = false;
            nextBuffVars_MoveSpeedMod = this.effect2[level];
            nextBuffVars_ArmorMod = this.effect3[level];
            foreach(Vector3 pos in GetPointsOnLine(ownerPos, targetPos, lineWidth, distance, iterations))
            {
                nextBuffVars_Pos = pos;
                AddBuff((ObjAIBase)owner, owner, new Buffs.WallOfPain(nextBuffVars_MoveSpeedMod, nextBuffVars_ArmorMod, nextBuffVars_Pos), 50, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0.1f, true, false, false);
                if(!foundFirstPos)
                {
                    firstPos = pos;
                    foundFirstPos = true;
                }
                lastPos = pos;
            }
            other1 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", firstPos, teamID, false, true, false, false, false, true, 300, false, true, (Champion)owner);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            other2 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", lastPos, teamID, false, true, false, false, false, true, 300, false, true, (Champion)owner);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other1, other2, new Buffs.WallOfPainBeam(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other2);
            other3 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", targetPos, teamID, false, true, false, false, false, true, 300 + lineWidth, false, true, (Champion)owner);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other3);
            LinkVisibility(other2, other3);
        }
    }
}