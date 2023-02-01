#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Tremors2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Tremors2",
            BuffTextureName = "Armordillo_RecklessCharge.dds",
        };
        float tremDamage;
        Particle tremorsFx;
        float lastTimeExecuted;
        public Tremors2(float tremDamage = default)
        {
            this.tremDamage = tremDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.tremDamage);
            SpellEffectCreate(out this.tremorsFx, out _, "Tremors_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
            {
                ApplyDamage(attacker, unit, this.tremDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 0, false, false, attacker);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.tremorsFx);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
                {
                    ApplyDamage(attacker, unit, this.tremDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 0, false, false, attacker);
                }
            }
        }
        public override void OnDeath()
        {
            SpellEffectRemove(this.tremorsFx);
        }
    }
}
namespace Spells
{
    public class Tremors2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {65, 130, 195};
        int[] effect1 = {8, 8, 8, 8, 8};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_TremDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_TremDamage = this.effect0[level];
            AddBuff(attacker, target, new Buffs.Tremors2(nextBuffVars_TremDamage), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}