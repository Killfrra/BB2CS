#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MissFortuneBulletTime : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.2f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        object castPosition; // UNITIALIZED
        float counter;
        public override void ChannelingStart()
        {
            Vector3 castPosition; // UNITIALIZED
            FaceDirection(owner, castPosition);
        }
        public override void ChannelingUpdateActions()
        {
            object castPosition; // UNUSED
            Particle goodluck; // UNUSED
            Vector3 point1;
            Vector3 point2;
            Vector3 point3;
            Vector3 point4;
            Vector3 point5;
            Vector3 point6;
            Vector3 point7; // UNUSED
            Vector3 point8;
            Vector3 point9; // UNUSED
            Vector3 point0;
            float count;
            float modValue;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            castPosition = this.castPosition;
            this.counter++;
            SpellEffectCreate(out goodluck, out _, "missFortune_ult_cas_muzzle_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_3", default, owner, default, default, false);
            SpellEffectCreate(out goodluck, out _, "missFortune_ult_cas_muzzle_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false);
            SpellEffectCreate(out goodluck, out _, "missFortune_left_ult_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_weapon", default, owner, default, default, false);
            SpellEffectCreate(out goodluck, out _, "missFortune_ult_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_weapon", default, owner, default, default, false);
            point1 = GetPointByUnitFacingOffset(owner, 500, 15);
            point2 = GetPointByUnitFacingOffset(owner, 500, 9);
            point3 = GetPointByUnitFacingOffset(owner, 500, 3);
            point4 = GetPointByUnitFacingOffset(owner, 500, 357);
            point5 = GetPointByUnitFacingOffset(owner, 500, 351);
            point6 = GetPointByUnitFacingOffset(owner, 500, 345);
            point7 = GetPointByUnitFacingOffset(owner, 500, 350);
            point8 = GetPointByUnitFacingOffset(owner, 500, 345);
            point9 = GetPointByUnitFacingOffset(owner, 500, 340);
            point0 = GetPointByUnitFacingOffset(owner, 500, 0);
            count = GetBuffCountFromAll(owner, nameof(Buffs.MissFortuneWaves));
            modValue = count % 2;
            if(modValue == 0)
            {
                SpellCast((ObjAIBase)owner, default, point1, point1, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point2, point2, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point3, point3, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point4, point4, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point5, point5, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point6, point6, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point0, point8, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
            if(modValue > 0)
            {
                SpellCast((ObjAIBase)owner, default, point1, point1, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point2, point2, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point3, point3, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point4, point4, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point5, point5, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point6, point6, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast((ObjAIBase)owner, default, point0, point8, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneWaves(), 8, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            count++;
            if(count >= 8)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Success, ChannelingStopSource.TimeCompleted);
            }
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.MissFortuneBulletSound), (ObjAIBase)owner);
            SpellBuffClear(owner, nameof(Buffs.MissFortuneWaves));
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.MissFortuneBulletSound), (ObjAIBase)owner);
            SpellBuffClear(owner, nameof(Buffs.MissFortuneWaves));
        }
    }
}