#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickSummonDecayed : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        float[] effect0 = {-0.1f, -0.125f, -0.15f, -0.175f, -0.2f};
        int[] effect1 = {60, 95, 130, 165, 200};
        float[] effect2 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID;
            Minion other1;
            float nextBuffVars_MoveSpeedMod;
            Particle e; // UNUSED
            float baseDamage;
            float yorickAD; // UNUSED
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            other1 = SpawnMinion("Inky", "YorickDecayedGhoul", "YorickPHPet.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, false, true, false, false, false, 0, false, false, (Champion)owner);
            AddBuff(other1, attacker, new Buffs.YorickSummonDecayed(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            AddBuff(attacker, other1, new Buffs.YorickDecayedDiseaseCloud(nextBuffVars_MoveSpeedMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.YorickDecayedLogic(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            SpellEffectCreate(out e, out _, "yorick_necroExplosion.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, default, other1, default, default, false, default, default, false, false);
            baseDamage = this.effect1[level];
            yorickAD = GetFlatPhysicalDamageMod(owner);
            nextBuffVars_MoveSpeedMod = this.effect2[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, other1.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle b; // UNUSED
                BreakSpellShields(unit);
                SpellEffectCreate(out b, out _, "yorick_necroExplosion_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                AddBuff((ObjAIBase)owner, unit, new Buffs.YorickDecayedSlow(nextBuffVars_MoveSpeedMod), 100, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                ApplyDamage((ObjAIBase)owner, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, false, (ObjAIBase)owner);
            }
        }
    }
}
namespace Buffs
{
    public class YorickSummonDecayed : BBBuffScript
    {
        bool isDead; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            bool nearbyChampion; // UNUSED
            bool checkBuilding; // UNUSED
            SetGhosted(attacker, true);
            nearbyChampion = false;
            checkBuilding = true;
            this.isDead = false;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Vector3 ghoulPosition;
            Particle b; // UNUSED
            teamID = GetTeamID(owner);
            ghoulPosition = GetUnitPosition(attacker);
            SpellEffectCreate(out b, out _, "yorick_necroExplosion_deactivate.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ghoulPosition, owner, default, default, true, default, default, false, false);
            ApplyDamage(attacker, attacker, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            float currentHealth;
            currentHealth = GetHealth(attacker, PrimaryAbilityResourceType.MANA);
            if(currentHealth <= 0)
            {
                this.isDead = true;
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                bool nearbyChampion;
                bool checkBuilding;
                nearbyChampion = false;
                checkBuilding = true;
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    ApplyTaunt(unit, attacker, 1.5f);
                    nearbyChampion = true;
                    checkBuilding = false;
                }
                if(!nearbyChampion)
                {
                    foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
                    {
                        ApplyTaunt(unit, attacker, 1.5f);
                        checkBuilding = false;
                    }
                }
                if(checkBuilding)
                {
                    foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectTurrets, 1, default, true))
                    {
                        ApplyTaunt(unit, attacker, 1.5f);
                    }
                }
            }
        }
    }
}