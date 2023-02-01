#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickSummonRavenous : BBBuffScript
    {
        bool isDead; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetGhosted(attacker, true);
            this.isDead = false;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Vector3 ghoulPosition;
            Particle b; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickActiveRavenous)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickActiveRavenous));
            }
            teamID = GetTeamID(owner);
            ghoulPosition = GetUnitPosition(attacker);
            SpellEffectCreate(out b, out _, "yorick_ravenousGhoul_death.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ghoulPosition, owner, default, default, true, default, default, false, false);
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
            bool isTaunted;
            bool nearbyChampion;
            bool checkBuilding;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.YorickRavenousPrimaryTarget), true))
                {
                    ApplyTaunt(unit, attacker, 1.5f);
                }
                isTaunted = GetTaunted(attacker);
                if(!isTaunted)
                {
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
namespace Spells
{
    public class YorickSummonRavenous : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Minion other1;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            other1 = SpawnMinion("Blinky", "YorickRavenousGhoul", "YorickPHPet.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
            AddBuff(other1, attacker, new Buffs.YorickSummonRavenous(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.YorickRavenousLogic(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}