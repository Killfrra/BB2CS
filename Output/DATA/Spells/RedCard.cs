#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RedCard : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "",
        };
        bool willRemove;
        Particle effectID;
        int[] effect0 = {30, 45, 60, 75, 90};
        float[] effect1 = {-0.7f, -0.7f, -0.7f, -0.7f, -0.7f};
        int[] effect2 = {0, 0, 0, 0, 0};
        int[] effect3 = {0, 0, 0, 0, 0};
        int[] effect4 = {0, 0, 0, 0, 0};
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
            SpellEffectCreate(out this.effectID, out _, "Card_Red_Tag.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SpellEffectRemove(this.effectID);
            baseCooldown = 4;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float attackDamage;
            float bonusDamage;
            float redCardDamage;
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackDamage = GetTotalAttackDamage(owner);
            bonusDamage = this.effect0[level];
            redCardDamage = attackDamage + bonusDamage;
            if(target is ObjAIBase)
            {
                Particle ar; // UNUSED
                SpellEffectCreate(out ar, out _, "Pulverize_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            }
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            nextBuffVars_AttackSpeedMod = this.effect2[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff((ObjAIBase)owner, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true);
                if(unit != target)
                {
                    DebugSay(owner, "YO!2");
                    ApplyDamage(attacker, unit, redCardDamage + this.effect3[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.4f, 1, false, false);
                }
                else
                {
                    ApplyDamage(attacker, unit, bonusDamage + this.effect4[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.4f, 1, false, false);
                    DebugSay(owner, "YO!");
                }
            }
            this.willRemove = true;
            damageAmount *= 0;
        }
    }
}