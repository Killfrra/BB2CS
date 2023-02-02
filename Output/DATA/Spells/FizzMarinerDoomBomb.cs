#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzMarinerDoomBomb : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Fizz_UltimateMissile_Orbit.troy", "", },
            BuffName = "FizzChurnTheWatersCling",
            BuffTextureName = "FizzMarinerDoom.dds",
        };
        Region tempID;
        Particle temp2;
        Particle temp;
        float tickDamage;
        float lastTimeExecuted;
        float[] effect0 = {-0.5f, -0.6f, -0.7f};
        int[] effect1 = {200, 325, 450};
        float[] effect2 = {-0.5f, -0.6f, -0.7f};
        public override void OnActivate()
        {
            TeamId teamID;
            int level;
            float nextBuffVars_MoveSpeedMod;
            teamID = GetTeamID(attacker);
            this.tempID = AddUnitPerceptionBubble(teamID, 300, owner, 4, default, default, false);
            SpellEffectCreate(out this.temp2, out this.temp, "Fizz_Ring_Green.troy", "Fizz_Ring_Red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, owner, default, default, true, false, true, false, false);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            AddBuff(attacker, owner, new Buffs.FizzMarinerDoomSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            this.tickDamage = 3;
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float nextBuffVars_MoveSpeedMod;
            Vector3 nextBuffVars_CenterPos;
            SpellEffectRemove(this.temp);
            SpellEffectRemove(this.temp2);
            RemovePerceptionBubble(this.tempID);
            if(expired)
            {
                TeamId teamID;
                Vector3 targetPos;
                Minion other3;
                Particle temp; // UNUSED
                AttackableUnit other1; // UNITIALIZED
                Vector3 groundPos;
                teamID = GetTeamID(attacker);
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, false, attacker);
                    nextBuffVars_MoveSpeedMod = this.effect2[level];
                    AddBuff(attacker, unit, new Buffs.FizzMarinerDoomSlow(nextBuffVars_MoveSpeedMod), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    if(unit != owner)
                    {
                        Vector3 ownerPos;
                        ownerPos = GetUnitPosition(owner);
                        nextBuffVars_CenterPos = ownerPos;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.FizzMoveback(nextBuffVars_CenterPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                    }
                    else
                    {
                        AddBuff(attacker, owner, new Buffs.FizzKnockup(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                    }
                }
                teamID = GetTeamID(attacker);
                targetPos = GetUnitPosition(owner);
                other3 = SpawnMinion("Omnomnomnom", "FizzShark", "idle.lua", targetPos, teamID ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, false, (Champion)attacker);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash.troy", "Fizz_SharkSplash.troy", teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, "BUFFBONE_CSTM_GROUND", targetPos, other1, "BUFFBONE_CSTM_GROUND", targetPos, true, false, false, false, false);
                groundPos = GetGroundHeight(targetPos);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash_Ground.troy ", "Fizz_SharkSplash_Ground.troy ", teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, groundPos, default, default, groundPos, true, false, false, false, false);
                AddBuff(other3, other3, new Buffs.FizzShark(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                float remDuration;
                Vector3 missileEndPosition;
                bool nextBuffVars_willStick;
                Vector3 nextBuffVars_MissilePosition;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                remDuration = GetBuffRemainingDuration(owner, nameof(Buffs.FizzMarinerDoomBomb));
                nextBuffVars_willStick = false;
                missileEndPosition = GetUnitPosition(owner);
                nextBuffVars_MissilePosition = missileEndPosition;
                AddBuff(attacker, attacker, new Buffs.FizzMarinerDoomMissile(nextBuffVars_willStick, nextBuffVars_MissilePosition), 1, 1, remDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                charVars.UltFired = false;
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                if(this.tickDamage > 0)
                {
                    float nextBuffVars_TickDamage;
                    nextBuffVars_TickDamage = this.tickDamage;
                    AddBuff(attacker, owner, new Buffs.TimeBombCountdown(nextBuffVars_TickDamage), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, owner, this.tickDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                    this.tickDamage--;
                }
            }
        }
    }
}