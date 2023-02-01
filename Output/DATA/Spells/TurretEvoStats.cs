#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretEvoStats : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "H28GEvolutionTurret",
            BuffTextureName = "Jester_DeathWard.dds",
        };
        float bonusDamage;
        float bonusHealth;
        float bonusArmor;
        float lastTimeExecuted;
        public TurretEvoStats(float bonusHealth = default)
        {
            this.bonusHealth = bonusHealth;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            Particle poofin; // UNUSED
            //RequireVar(this.bonusDamage);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.bonusArmor);
            SetCanMove(owner, false);
            SpellEffectCreate(out poofin, out _, "jackintheboxpoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, this.bonusHealth);
            IncFlatPhysicalDamageMod(owner, this.bonusDamage);
            IncFlatArmorMod(owner, this.bonusArmor);
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1, default, true))
                {
                    ApplyTaunt(unit, owner, 0.5f);
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    this.bonusDamage += 0.125f;
                    this.bonusArmor += 0.125f;
                }
            }
        }
    }
}