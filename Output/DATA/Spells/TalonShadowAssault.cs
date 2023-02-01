#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TalonShadowAssault : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        Particle particleZ; // UNUSED
        public override void SelfExecute()
        {
            Vector3 pos;
            SpellEffectCreate(out this.particleZ, out _, "talon_ult_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, attacker, default, default, false, false, false, false, true);
            SpellEffectCreate(out this.particleZ, out _, "talon_invis_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, attacker, default, default, false, false, false, false, true);
            AddBuff(attacker, target, new Buffs.TalonShadowAssaultAnimBuff(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.TalonShadowAssaultBuff(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.TalonShadowAssaultMisOne(), 1, 1, 10000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 0);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 135);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, -90);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 45);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 180);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, -45);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 90);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, -135);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.TalonShadowAssaultToggle));
            SetCanCast(owner, false);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(0.5f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}