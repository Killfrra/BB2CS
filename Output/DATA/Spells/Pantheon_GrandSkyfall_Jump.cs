#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Pantheon_GrandSkyfall_Jump : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 1.5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Vector3 targetPos; // UNITIALIZED
        Particle part; // UNUSED
        float[] effect0 = {90, 166.67f, 235.33f};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            targetPos = GetCastSpellTargetPos();
            targetPos = GetNearestPassablePosition(owner, targetPos);
            charVars.TargetPos = targetPos;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GrandSkyfall_Jump(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            FaceDirection(owner, targetPos);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
                {
                    int count;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                    count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                    if(count >= 4)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                    }
                }
            }
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID;
            Vector3 targetPos; // UNUSED
            Vector3 nextBuffVars_TargetPos;
            Particle b; // UNUSED
            float smnCooldown0;
            float smnCooldown1;
            object nextBuffVars_Particle; // UNUSED
            teamID = GetTeamID(owner);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GrandSkyfall_Jump), (ObjAIBase)owner, 0);
            targetPos = charVars.TargetPos;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_TargetPos = this.targetPos;
            SetCanCast(owner, true);
            SpellCast((ObjAIBase)owner, default, this.targetPos, this.targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            SpellEffectCreate(out this.part, out b, "pantheon_grandskyfall_up.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, default, default, false, false);
            nextBuffVars_Particle = charVars.Particle;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GrandSkyfall(nextBuffVars_TargetPos), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            smnCooldown0 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            smnCooldown1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GS_ParticleRed(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(smnCooldown0 < 2.75f)
            {
                SetSlotSpellCooldownTimeVer2(2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner, false);
            }
            if(smnCooldown1 < 2.75f)
            {
                SetSlotSpellCooldownTimeVer2(2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner, false);
            }
        }
        public override void ChannelingCancelStop()
        {
            float manaRefund;
            SetSlotSpellCooldownTimeVer2(10, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            manaRefund = this.effect0[level];
            IncPAR(owner, manaRefund, PrimaryAbilityResourceType.MANA);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GrandSkyfall_Jump), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_Particle), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_ParticleRed), (ObjAIBase)owner, 0);
        }
    }
}
namespace Buffs
{
    public class Pantheon_GrandSkyfall_Jump : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Pantheon Grand Skyfall",
            BuffTextureName = "Pantheon_GrandSkyfall.dds",
        };
        Particle part;
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            Vector3 targetPos; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.part, out a, "pantheon_grandskyfall_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, default, default, false, false);
            targetPos = charVars.TargetPos;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GS_Particle(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.part);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}