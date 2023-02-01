#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenSlash3 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "GarenSlash",
            BuffTextureName = "Garen_DecisiveStrike.dds",
        };
        Particle geeves1;
        Particle geeves2;
        float spellCooldown;
        public GarenSlash3(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.geeves1, out _, "garen_descisiveStrike_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_2", default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.geeves2, out _, "garen_descisiveStrike_indicator_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_2", default, owner, default, default, true, false, false, false, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.silenceDuration);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
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
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(this.geeves1);
            SpellEffectRemove(this.geeves2);
        }
        public override void OnPreAttack()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SkipNextAutoAttack(owner);
            SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.GarenSlash3), (ObjAIBase)owner, 0);
        }
    }
}
namespace Spells
{
    public class GarenSlash3 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        int[] effect1 = {12, 11, 10, 9, 8};
        float[] effect2 = {1.5f, 2, 2.5f, 3, 3.5f};
        int[] effect3 = {30, 45, 60, 75, 90};
        public override void SelfExecute()
        {
            float nextBuffVars_SpeedMod;
            int nextBuffVars_SpellCooldown;
            float nextBuffVars_SilenceDuration;
            int nextBuffVars_BonusDamage;
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            nextBuffVars_SpeedMod = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenFastMove(nextBuffVars_SpeedMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_SpellCooldown = this.effect1[level];
            nextBuffVars_SilenceDuration = this.effect2[level];
            nextBuffVars_BonusDamage = this.effect3[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenSlash3(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}