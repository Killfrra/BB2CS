#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzMarinerDoomMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PersistsThroughDeath = true,
        };
        bool willStick;
        Vector3 missilePosition;
        Particle temp4;
        Particle temp3;
        Particle temp;
        Particle temp2;
        Region tempVision; // UNUSED
        bool exploded;
        int[] effect0 = {200, 325, 450};
        float[] effect1 = {-0.5f, -0.6f, -0.7f};
        public FizzMarinerDoomMissile(bool willStick = default, Vector3 missilePosition = default)
        {
            this.willStick = willStick;
            this.missilePosition = missilePosition;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 groundPos;
            //RequireVar(this.missilePosition);
            //RequireVar(this.willStick);
            teamID = GetTeamID(owner);
            groundPos = GetGroundHeight(this.missilePosition);
            SpellEffectCreate(out this.temp4, out this.temp3, "Fizz_Ring_Green.troy", "Fizz_Ring_Red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "head", groundPos, default, default, this.missilePosition, false, false, false, false, false);
            SpellEffectCreate(out this.temp, out this.temp2, "Fizz_UltimateMissile_Orbit.troy", "Fizz_UltimateMissile_Orbit.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "head", groundPos, default, default, this.missilePosition, false, false, false, false, false);
            this.tempVision = AddPosPerceptionBubble(teamID, 350, this.missilePosition, 3, default, false);
            this.exploded = false;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            int level;
            Vector3 nextBuffVars_CenterPos;
            float nextBuffVars_MoveSpeedMod;
            Minion other1;
            Vector3 groundPos;
            Particle temp; // UNUSED
            SpellEffectRemove(this.temp);
            SpellEffectRemove(this.temp2);
            SpellEffectRemove(this.temp3);
            SpellEffectRemove(this.temp4);
            if(!this.exploded)
            {
                teamID = GetTeamID(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, this.missilePosition, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, false, (ObjAIBase)owner);
                    nextBuffVars_CenterPos = this.missilePosition;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.FizzMoveback(nextBuffVars_CenterPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                    nextBuffVars_MoveSpeedMod = this.effect1[level];
                    AddBuff(attacker, unit, new Buffs.FizzMarinerDoomSlow(nextBuffVars_MoveSpeedMod), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                other1 = SpawnMinion("OMNOMNOMNOMONOM", "FizzShark", "idle.lua", this.missilePosition, teamID, true, true, true, true, true, true, 100, true, false, (Champion)owner);
                groundPos = GetGroundHeight(this.missilePosition);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash.troy", "Fizz_SharkSplash.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, groundPos, default, default, groundPos, true, false, false, false, false);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash_Ground.troy ", "Fizz_SharkSplash_Ground.troy ", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, groundPos, default, default, groundPos, true, false, false, false, false);
                AddBuff(other1, other1, new Buffs.FizzShark(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                this.exploded = true;
            }
        }
        public override void OnUpdateActions()
        {
            float duration;
            if(this.willStick)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, this.missilePosition, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    duration = GetBuffRemainingDuration(owner, nameof(Buffs.FizzMarinerDoomMissile));
                    SpellEffectRemove(this.temp);
                    SpellEffectRemove(this.temp2);
                    SpellEffectRemove(this.temp3);
                    SpellEffectRemove(this.temp4);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.FizzMarinerDoomBomb(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    this.exploded = true;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}
namespace Spells
{
    public class FizzMarinerDoomMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        bool ultFired;
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            Vector3 nextBuffVars_MissilePosition;
            bool nextBuffVars_willStick;
            if(this.ultFired)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_MissilePosition = missileEndPosition;
                nextBuffVars_willStick = true;
                AddBuff((ObjAIBase)owner, owner, new Buffs.FizzMarinerDoomMissile(nextBuffVars_willStick, nextBuffVars_MissilePosition), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                charVars.UltFired = false;
                this.ultFired = false;
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 nextBuffVars_MissilePosition;
            bool nextBuffVars_willStick;
            Vector3 missileEndPosition;
            DestroyMissile(missileNetworkID);
            BreakSpellShields(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AddBuff(attacker, target, new Buffs.FizzMarinerDoomBomb(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            charVars.UltFired = false;
            if(GetBuffCountFromCaster(target, default, nameof(Buffs.FizzMarinerDoomBomb)) == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                missileEndPosition = GetUnitPosition(target);
                nextBuffVars_MissilePosition = missileEndPosition;
                nextBuffVars_willStick = false;
                AddBuff((ObjAIBase)owner, owner, new Buffs.FizzMarinerDoomMissile(nextBuffVars_willStick, nextBuffVars_MissilePosition), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}