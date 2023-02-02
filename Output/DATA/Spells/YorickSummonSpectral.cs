#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickSummonSpectral : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        int[] effect1 = {8, 16, 24, 32, 40};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID;
            Minion other1;
            float nextBuffVars_MovementSpeedPercent;
            float nextBuffVars_AttackDamageMod;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            other1 = SpawnMinion("Clyde", "YorickSpectralGhoul", "YorickPHPet.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, false, true, false, false, false, 0, false, false, (Champion)owner);
            nextBuffVars_MovementSpeedPercent = this.effect0[level];
            nextBuffVars_AttackDamageMod = this.effect1[level];
            AddBuff(other1, attacker, new Buffs.YorickSummonSpectral(nextBuffVars_AttackDamageMod, nextBuffVars_MovementSpeedPercent), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.YorickSpectralLogic(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.YorickActiveSpectral(nextBuffVars_MovementSpeedPercent), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class YorickSummonSpectral : BBBuffScript
    {
        Particle spectraFX; // UNUSED
        float attackDamageMod;
        float movementSpeedPercent;
        bool isDead; // UNUSED
        float lastTimeExecuted;
        public YorickSummonSpectral(float attackDamageMod = default, float movementSpeedPercent = default)
        {
            this.attackDamageMod = attackDamageMod;
            this.movementSpeedPercent = movementSpeedPercent;
        }
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            SpellEffectCreate(out this.spectraFX, out _, "YorickPHSpectral.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            //RequireVar(this.attackDamageMod);
            //RequireVar(this.movementSpeedPercent);
            this.isDead = false;
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 ghoulPosition;
            TeamId teamID;
            Particle a; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickActiveSpectral)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickActiveSpectral));
            }
            ghoulPosition = GetUnitPosition(attacker);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "yorick_spectralGhoul_death.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ghoulPosition, owner, default, ghoulPosition, true, default, default, false, false);
            ApplyDamage(attacker, attacker, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            float currentHealth;
            IncFlatPhysicalDamageMod(attacker, this.attackDamageMod);
            IncPercentMovementSpeedMod(attacker, this.movementSpeedPercent);
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
                bool isTaunted;
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.YorickSpectralPrimaryTarget), true))
                {
                    ApplyTaunt(unit, attacker, 1.5f);
                }
                isTaunted = GetTaunted(attacker);
                if(!isTaunted)
                {
                    bool nearbyChampion;
                    bool checkBuilding;
                    nearbyChampion = false;
                    checkBuilding = true;
                    foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 1050, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                    {
                        ApplyTaunt(unit, attacker, 1.5f);
                        nearbyChampion = true;
                        checkBuilding = false;
                    }
                    if(!nearbyChampion)
                    {
                        foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions, 1, default, true))
                        {
                            ApplyTaunt(unit, attacker, 1.5f);
                            checkBuilding = false;
                        }
                    }
                    if(checkBuilding)
                    {
                        foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectBuildings, 1, default, true))
                        {
                            ApplyTaunt(unit, attacker, 1.5f);
                        }
                    }
                }
            }
        }
    }
}