#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LeonaSolarBarrier : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {30, 40, 50, 60, 70};
        int[] effect1 = {60, 110, 160, 210, 260};
        public override void SelfExecute()
        {
            float nextBuffVars_DefenseBonus;
            int nextBuffVars_MagicDamage;
            nextBuffVars_DefenseBonus = this.effect0[level];
            nextBuffVars_MagicDamage = this.effect1[level];
            AddBuff(attacker, attacker, new Buffs.LeonaSolarBarrier(nextBuffVars_MagicDamage, nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeonaSolarBarrier : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "LeonaSolarBarrier",
            BuffTextureName = "LeonaSolarBarrier.dds",
            SpellToggleSlot = 2,
        };
        float magicDamage;
        float defenseBonus;
        Particle particle;
        public LeonaSolarBarrier(float magicDamage = default, float defenseBonus = default)
        {
            this.magicDamage = magicDamage;
            this.defenseBonus = defenseBonus;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            //RequireVar(this.magicDamage);
            //RequireVar(this.defenseBonus);
            IncFlatArmorMod(owner, this.defenseBonus);
            IncFlatSpellBlockMod(owner, this.defenseBonus);
            SpellEffectCreate(out this.particle, out _, "Leona_SolarBarrier_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            OverrideAnimation("Idle1", "Spell2_idle", owner);
            OverrideAnimation("Idle2", "Spell2_idle", owner);
            OverrideAnimation("Idle3", "Spell2_idle", owner);
            OverrideAnimation("Idle4", "Spell2_idle", owner);
            OverrideAnimation("Attack1", "Spell2_attack", owner);
            OverrideAnimation("Attack2", "Spell2_attack", owner);
            OverrideAnimation("Attack3", "Spell2_attack", owner);
            OverrideAnimation("Crit", "Spell2_attack", owner);
            OverrideAnimation("Run", "Spell2_run", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            if(!owner.IsDead)
            {
                bool targetStruck;
                float magicDamage;
                TeamId teamID;
                Particle temp; // UNUSED
                targetStruck = false;
                magicDamage = this.magicDamage;
                teamID = GetTeamID(owner);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    Particle targetParticle; // UNUSED
                    targetStruck = true;
                    BreakSpellShields(unit);
                    AddBuff(attacker, unit, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    ApplyDamage(attacker, unit, magicDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                    SpellEffectCreate(out targetParticle, out _, "Leona_SolarBarrier_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                }
                SpellEffectRemove(this.particle);
                ClearOverrideAnimation("Idle1", owner);
                ClearOverrideAnimation("Idle2", owner);
                ClearOverrideAnimation("Idle3", owner);
                ClearOverrideAnimation("Idle4", owner);
                ClearOverrideAnimation("Attack1", owner);
                ClearOverrideAnimation("Attack2", owner);
                ClearOverrideAnimation("Attack3", owner);
                ClearOverrideAnimation("Crit", owner);
                ClearOverrideAnimation("Run", owner);
                if(targetStruck)
                {
                    float nextBuffVars_DefenseBonus;
                    SpellEffectCreate(out temp, out _, "Leona_SolarBarrier_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, default, default, false, false);
                    nextBuffVars_DefenseBonus = this.defenseBonus;
                    AddBuff(attacker, attacker, new Buffs.LeonaSolarBarrier2(nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    SpellEffectCreate(out temp, out _, "Leona_SolarBarrier_nova_whiff.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, default, default, false, false);
                }
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defenseBonus);
            IncFlatSpellBlockMod(owner, this.defenseBonus);
        }
    }
}