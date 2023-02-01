#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JaxEmpowerTwo : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "EmpowerTwo",
            BuffTextureName = "Armsmaster_Empower.dds",
        };
        Particle particle;
        float spellCooldown;
        int[] effect0 = {40, 85, 130, 175, 220};
        public JaxEmpowerTwo(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "armsmaster_empower_self_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, "weapon", default, false, false, false, false, false);
            SpellEffectCreate(out this.particle, out _, "armsmaster_empower_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            spellCooldown = this.spellCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * spellCooldown;
            SpellEffectRemove(this.particle);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, false);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID;
            int level;
            if(target is BaseTurret)
            {
            }
            else
            {
                teamID = GetTeamID(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectRemove(this.particle);
                SpellEffectCreate(out this.particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                SpellEffectCreate(out this.particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.JaxEmpowerTwo), (ObjAIBase)owner, 0);
                SetDodgePiercing(owner, false);
            }
        }
    }
}
namespace Spells
{
    public class JaxEmpowerTwo : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {9, 7.5f, 6, 4.5f, 3};
        int[] effect1 = {20, 50, 80, 110, 140};
        public override void SelfExecute()
        {
            float nextBuffVars_SpellCooldown;
            int nextBuffVars_BonusDamage;
            nextBuffVars_SpellCooldown = this.effect0[level];
            nextBuffVars_BonusDamage = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxEmpowerTwo(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}