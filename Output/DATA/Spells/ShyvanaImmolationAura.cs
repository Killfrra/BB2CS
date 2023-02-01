#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaImmolationAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", },
            AutoBuffActivateEffect = new[]{ "shyvana_scorchedEarth_01.troy", "shyvana_scorchedEarth_speed.troy", },
            BuffName = "ShyvanaScorchedEarth",
            BuffTextureName = "ShyvanaScorchedEarth.dds",
        };
        float damagePerTick;
        float movementSpeed;
        float lastTimeExecuted;
        float[] effect0 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        int[] effect1 = {25, 40, 55, 70, 85};
        public ShyvanaImmolationAura(float damagePerTick = default, float movementSpeed = default)
        {
            this.damagePerTick = damagePerTick;
            this.movementSpeed = movementSpeed;
        }
        public override void OnActivate()
        {
            float bonusAD;
            TeamId teamID;
            Particle a; // UNUSED
            //RequireVar(this.damagePerTick);
            //RequireVar(this.movementSpeed);
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 0.2f;
            this.damagePerTick += bonusAD;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                teamID = GetTeamID(attacker);
                SpellEffectCreate(out a, out _, "shyvana_scorchedEarth_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
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
            TeamId teamID;
            Particle a; // UNUSED
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out a, out _, "shyvana_scorchedEarth_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
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
            float nextBuffVars_DamagePerTick;
            if(charVars.HitCount < 5)
            {
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaImmolationAura));
                newDuration = remainingDuration + 1;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_MovementSpeed = this.effect0[level];
                nextBuffVars_DamagePerTick = this.effect1[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolationAura(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                charVars.HitCount++;
            }
        }
    }
}
namespace Spells
{
    public class ShyvanaImmolationAura : BBSpellScript
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
            float nextBuffVars_DamagePerTick;
            nextBuffVars_MovementSpeed = this.effect0[level];
            nextBuffVars_DamagePerTick = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolationAura(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}