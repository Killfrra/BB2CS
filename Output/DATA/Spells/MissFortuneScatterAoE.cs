#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortuneScatterAoE : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        int rainCount; // UNUSED
        float damage;
        float totalDamage;
        float moveSpeedMod;
        int attackSpeedMod;
        float lastTimeExecuted;
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        int[] effect1 = {0, 0, 0, 0, 0};
        public MissFortuneScatterAoE(float damage = default)
        {
            this.damage = damage;
        }
        public override void OnActivate()
        {
            int level;
            TeamId teamOfOwner; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            this.rainCount = 1;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.damage);
            this.totalDamage = this.damage / 7;
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            teamOfOwner = GetTeamID(owner);
            this.moveSpeedMod = this.effect0[level];
            this.attackSpeedMod = this.effect1[level];
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            nextBuffVars_AttackSpeedMod = this.moveSpeedMod;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle asdf; // UNUSED
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                ApplyDamage(attacker, unit, this.totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.114f, 1, false, false, attacker);
                SpellEffectCreate(out asdf, out _, "missFortune_makeItRain_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            int level; // UNUSED
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MoveSpeedMod;
                    Particle asdf; // UNUSED
                    float nextBuffVars_AttackSpeedMod;
                    nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                    nextBuffVars_AttackSpeedMod = this.attackSpeedMod;
                    AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                    ApplyDamage(attacker, unit, this.totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.114f, 1, false, false, attacker);
                    SpellEffectCreate(out asdf, out _, "missFortune_makeItRain_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                }
            }
        }
    }
}