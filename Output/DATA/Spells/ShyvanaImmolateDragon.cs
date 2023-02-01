#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaImmolateDragon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", },
            AutoBuffActivateEffect = new[]{ "shyvana_scorchedEarth_dragon_01.troy", "shyvana_scorchedEarth_speed.troy", },
            BuffName = "ShyvanaScorchedEarthDragon",
            BuffTextureName = "ShyvanaScorchedEarth.dds",
        };
        float damagePerTick;
        float movementSpeed;
        Vector3 lastPosition;
        float lastTimeExecuted;
        float[] effect0 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        int[] effect1 = {25, 40, 55, 70, 85};
        public ShyvanaImmolateDragon(float damagePerTick = default, float movementSpeed = default)
        {
            this.damagePerTick = damagePerTick;
            this.movementSpeed = movementSpeed;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 curPos;
            float nextBuffVars_DamagePerTick;
            Minion other3;
            //RequireVar(this.damagePerTick);
            //RequireVar(this.movementSpeed);
            teamID = GetTeamID(owner);
            curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            nextBuffVars_DamagePerTick = this.damagePerTick;
            other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)owner);
            AddBuff((ObjAIBase)owner, other3, new Buffs.ShyvanaIDApplicator(nextBuffVars_DamagePerTick), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            this.lastPosition = curPos;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.ShyvanaIDDamage)) == 0)
                {
                    nextBuffVars_DamagePerTick = this.damagePerTick;
                    AddBuff(attacker, unit, new Buffs.ShyvanaIDDamage(nextBuffVars_DamagePerTick), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            charVars.HitCount = 0;
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.movementSpeed);
        }
        public override void OnUpdateActions()
        {
            Vector3 curPos;
            float distance;
            TeamId teamID;
            float nextBuffVars_DamagePerTick;
            Minion other3;
            curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            distance = DistanceBetweenPoints(curPos, this.lastPosition);
            if(distance >= 150)
            {
                teamID = GetTeamID(attacker);
                nextBuffVars_DamagePerTick = this.damagePerTick;
                other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)attacker);
                AddBuff(attacker, other3, new Buffs.ShyvanaIDApplicator(nextBuffVars_DamagePerTick), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                this.lastPosition = curPos;
            }
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.ShyvanaIDDamage)) == 0)
                    {
                        nextBuffVars_DamagePerTick = this.damagePerTick;
                        AddBuff(attacker, unit, new Buffs.ShyvanaIDDamage(nextBuffVars_DamagePerTick), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                this.movementSpeed *= 0.85f;
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float remainingDuration;
            float newDuration;
            int level;
            float nextBuffVars_MovementSpeed;
            int nextBuffVars_DamagePerTick;
            if(charVars.HitCount < 4)
            {
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaImmolateDragon));
                newDuration = remainingDuration + 1;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_MovementSpeed = this.effect0[level];
                nextBuffVars_DamagePerTick = this.effect1[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolateDragon(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                charVars.HitCount++;
            }
        }
    }
}
namespace Spells
{
    public class ShyvanaImmolateDragon : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        int[] effect1 = {25, 40, 55, 70, 85};
        public override void SelfExecute()
        {
            float nextBuffVars_MovementSpeed;
            int nextBuffVars_DamagePerTick;
            nextBuffVars_MovementSpeed = this.effect0[level];
            nextBuffVars_DamagePerTick = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolateDragon(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}