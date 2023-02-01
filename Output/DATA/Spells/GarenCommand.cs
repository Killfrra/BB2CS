#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenCommand : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "GarenCommand",
            BuffTextureName = "Garen_CommandingPresence.dds",
        };
        float damageReduction;
        Particle particle;
        float totalArmorAmount;
        public GarenCommand(float damageReduction = default, float totalArmorAmount = default)
        {
            this.damageReduction = damageReduction;
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            int level; // UNUSED
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.damageReduction);
            IncPercentMagicReduction(owner, this.damageReduction);
            SpellEffectCreate(out this.particle, out _, "garen_commandingPresence_unit_buf_self.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, default, default, false, false);
            SetBuffToolTipVar(1, this.totalArmorAmount);
            IncPercentPhysicalReduction(owner, this.damageReduction);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, this.damageReduction);
            IncPercentMagicReduction(owner, this.damageReduction);
        }
    }
}
namespace Spells
{
    public class GarenCommand : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        Particle particle; // UNUSED
        int[] effect0 = {65, 100, 135, 170, 205};
        float[] effect1 = {0.2f, 0.24f, 0.28f, 0.32f, 0.36f};
        int[] effect2 = {3, 3, 3, 3, 3};
        public override void SelfExecute()
        {
            TeamId teamID;
            float abilityPower;
            float armorAmount;
            float nextBuffVars_DamageReduction;
            float nextBuffVars_TotalArmorAmount;
            float buffDuration;
            float totalArmorAmount;
            teamID = GetTeamID(owner);
            abilityPower = GetFlatMagicDamageMod(attacker);
            armorAmount = this.effect0[level];
            nextBuffVars_DamageReduction = this.effect1[level];
            buffDuration = this.effect2[level];
            abilityPower *= 0.8f;
            totalArmorAmount = abilityPower + armorAmount;
            nextBuffVars_TotalArmorAmount = totalArmorAmount;
            SpellEffectCreate(out this.particle, out _, "garen_command_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "C_BUFFBONE_GLB_CENTER_LOC", default, attacker, default, default, true, default, default, false, false);
            AddBuff(attacker, attacker, new Buffs.GarenCommand(nextBuffVars_DamageReduction, nextBuffVars_TotalArmorAmount), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}