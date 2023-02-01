#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCreepingDeath : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "MordekaiserCreepingDeathBuff",
            BuffTextureName = "MordekaiserCreepingDeath.dds",
        };
        float damagePerTick;
        float defenseStats;
        Particle b;
        float lastTimeExecuted;
        public MordekaiserCreepingDeath(float damagePerTick = default, float defenseStats = default)
        {
            this.damagePerTick = damagePerTick;
            this.defenseStats = defenseStats;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            int mordekaiserSkinID;
            teamID = GetTeamID(owner);
            //RequireVar(this.damagePerTick);
            //RequireVar(this.defenseStats);
            ApplyAssistMarker(attacker, owner, 10);
            IncPermanentFlatArmorMod(owner, this.defenseStats);
            IncPermanentFlatSpellBlockMod(owner, this.defenseStats);
            if(owner != attacker)
            {
                mordekaiserSkinID = GetSkinID(attacker);
                if(mordekaiserSkinID == 1)
                {
                    SpellEffectCreate(out this.b, out _, "mordekaiser_creepingDeath_auraGold.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else if(mordekaiserSkinID == 2)
                {
                    SpellEffectCreate(out this.b, out _, "mordekaiser_creepingDeath_auraRed.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out this.b, out _, "mordekaiser_creepingDeath_aura.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
            }
            else
            {
                SpellEffectCreate(out this.b, out _, "mordekaiser_creepingDeath_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float defenseStats;
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.MordekaiserCreepingDeathCheck)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.MordekaiserCreepingDeathCheck));
            }
            defenseStats = this.defenseStats * -1;
            IncPermanentFlatArmorMod(owner, defenseStats);
            IncPermanentFlatSpellBlockMod(owner, defenseStats);
            SpellEffectRemove(this.b);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_DamagePerTick;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    nextBuffVars_DamagePerTick = this.damagePerTick;
                    AddBuff((ObjAIBase)unit, attacker, new Buffs.MordekaiserCreepingDeathDebuff(nextBuffVars_DamagePerTick), 100, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class MordekaiserCreepingDeath : BBSpellScript
    {
        int[] effect0 = {24, 38, 52, 66, 80};
        int[] effect1 = {10, 15, 20, 25, 30};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamagePerTick;
            float nextBuffVars_DefenseStats;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_DamagePerTick = this.effect0[level];
            nextBuffVars_DefenseStats = this.effect1[level];
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.MordekaiserCreepingDeathCheck)) > 0)
            {
                if(target.Team == owner.Team)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.MordekaiserCreepingDeath(nextBuffVars_DamagePerTick, nextBuffVars_DefenseStats), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else
            {
                if(target.Team != owner.Team)
                {
                    AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserCreepingDeathDebuff(nextBuffVars_DamagePerTick), 100, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}