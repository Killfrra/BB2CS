#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVGoldenAegis : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "JarvanIVGoldenAegis",
            BuffTextureName = "JarvanIV_GoldenAegis.dds",
            OnPreDamagePriority = 3,
            SpellToggleSlot = 2,
        };
        Particle particle1;
        float shield;
        float oldArmorAmount;
        public JarvanIVGoldenAegis(float shield = default)
        {
            this.shield = shield;
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            SpellEffectCreate(out this.particle1, out _, "JarvanGoldenAegis_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetBuffToolTipVar(1, this.shield);
            IncreaseShield(owner, this.shield, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
            if(this.shield > 0)
            {
                RemoveShield(owner, this.shield, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.shield;
            if(this.shield >= damageAmount)
            {
                this.shield -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.shield;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.shield;
                this.shield = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
            SetBuffToolTipVar(1, this.shield);
        }
    }
}
namespace Spells
{
    public class JarvanIVGoldenAegis : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {50, 90, 130, 170, 210};
        int[] effect1 = {20, 25, 30, 35, 40};
        float[] effect2 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        public override void SelfExecute()
        {
            float shieldAmount;
            float shieldBonus;
            float bonusShield;
            float shield;
            float nextBuffVars_Shield;
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            Particle a; // UNUSED
            Particle asdf; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            shieldAmount = this.effect0[level];
            shieldBonus = this.effect1[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                bonusShield += shieldBonus;
            }
            shield = shieldAmount + bonusShield;
            nextBuffVars_Shield = shield;
            AddBuff(attacker, attacker, new Buffs.JarvanIVGoldenAegis(nextBuffVars_Shield), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_MoveSpeedMod = this.effect2[level];
            nextBuffVars_AttackSpeedMod = 0;
            SpellEffectCreate(out a, out _, "JarvanGoldenAegis_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                SpellEffectCreate(out asdf, out _, "JarvanGoldenAegis_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, unit, "spine", default, unit, default, default, true, default, default, false);
            }
        }
    }
}