#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaShieldOfDaybreak : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "LeonaShieldOfDaybreak",
            BuffTextureName = "LeonaShieldOfDaybreak.DDS",
        };
        Particle temp;
        float spellCooldown;
        float rangeIncrease;
        public LeonaShieldOfDaybreak(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.temp, out _, "Leona_ShieldOfDaybreak_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_SHIELD_TOP", default, owner, default, default, true, default, default, false, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.silenceDuration);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, true);
            this.rangeIncrease = 0;
            IncFlatAttackRangeMod(owner, 30 + this.rangeIncrease);
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
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(this.temp);
        }
        public override void OnUpdateStats()
        {
            IncFlatAttackRangeMod(owner, 30 + this.rangeIncrease);
        }
        public override void OnPreAttack()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SkipNextAutoAttack(owner);
            SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.LeonaShieldOfDaybreak), (ObjAIBase)owner, 0);
        }
    }
}
namespace Spells
{
    public class LeonaShieldOfDaybreak : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {12, 11, 10, 9, 8};
        int[] effect1 = {1, 1, 1, 1, 1};
        int[] effect2 = {35, 55, 75, 95, 115};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            int nextBuffVars_SilenceDuration;
            int nextBuffVars_BonusDamage;
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            nextBuffVars_SpellCooldown = this.effect0[level];
            nextBuffVars_SilenceDuration = this.effect1[level];
            nextBuffVars_BonusDamage = this.effect2[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeonaShieldOfDaybreak(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}