#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XenZhaoBattleCry : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void SelfExecute()
        {
            float nextBuffVars_SelfASMod;
            nextBuffVars_SelfASMod = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoBattleCry(nextBuffVars_SelfASMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class XenZhaoBattleCry : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "XenZhaoBattleCry",
            BuffTextureName = "XinZhao_BattleCry.dds",
        };
        float selfASMod;
        Particle battleCryPH; // UNUSED
        Particle battleCries;
        public XenZhaoBattleCry(float selfASMod = default)
        {
            this.selfASMod = selfASMod;
        }
        public override void OnActivate()
        {
            float cDTimer;
            int level; // UNUSED
            TeamId teamID;
            cDTimer = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AddBuff(attacker, target, new Buffs.XenZhaoBattleCryPH(), 1, 1, cDTimer, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.selfASMod);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.battleCryPH, out _, "xen_ziou_battleCry_cas_05.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.battleCryPH, out _, "xen_ziou_battleCry_cas_03.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.battleCryPH, out _, "xen_ziou_battleCry_cas_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_HAND_LOC", default, owner, default, default, true, default, default, false);
            SpellEffectCreate(out this.battleCryPH, out _, "xenZhiou_battleCry_active.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_HAND_LOC", default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.battleCryPH, out _, "xenZhiou_battleCry_active.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_HAND_LOC", default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.battleCries, out _, "xenZiou_battle_cry_weapon_01.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_4", default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.battleCries);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.selfASMod);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float cD0;
            float slot0CD;
            float cD2;
            float slot2CD;
            float cD3;
            float slot3CD;
            cD0 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            slot0CD = cD0 - 1;
            SetSlotSpellCooldownTimeVer2(slot0CD, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cD2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            slot2CD = cD2 - 1;
            SetSlotSpellCooldownTimeVer2(slot2CD, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cD3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            slot3CD = cD3 - 1;
            SetSlotSpellCooldownTimeVer2(slot3CD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}