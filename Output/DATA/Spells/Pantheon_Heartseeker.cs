#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_Heartseeker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Heartseeker",
        };
        Vector3 castPosition;
        Vector3 sourcePosition;
        float ticksRemaining;
        float lastTimeExecuted;
        public Pantheon_Heartseeker(Vector3 castPosition = default, Vector3 sourcePosition = default)
        {
            this.castPosition = castPosition;
            this.sourcePosition = sourcePosition;
        }
        public override void OnActivate()
        {
            int level;
            Vector3 castPosition;
            Vector3 sourcePosition;
            //RequireVar(this.castPosition);
            //RequireVar(this.sourcePosition);
            this.ticksRemaining = 2;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            castPosition = this.castPosition;
            sourcePosition = this.sourcePosition;
            SpellCast((ObjAIBase)owner, default, castPosition, castPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false, sourcePosition);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            Vector3 castPosition;
            Vector3 sourcePosition;
            if(this.ticksRemaining >= 1)
            {
                if(expired)
                {
                    this.ticksRemaining--;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    castPosition = this.castPosition;
                    sourcePosition = this.sourcePosition;
                    SpellCast((ObjAIBase)owner, default, castPosition, castPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false, sourcePosition);
                }
            }
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerSound), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_Heartseeker), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerChannel), (ObjAIBase)owner, 0);
        }
        public override void OnUpdateActions()
        {
            int level;
            Vector3 castPosition;
            Vector3 sourcePosition;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                if(this.ticksRemaining >= 1)
                {
                    this.ticksRemaining--;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    castPosition = this.castPosition;
                    sourcePosition = this.sourcePosition;
                    SpellCast((ObjAIBase)owner, default, castPosition, castPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false, sourcePosition);
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}
namespace Spells
{
    public class Pantheon_Heartseeker : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 0.75f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
        };
        public override void ChannelingStart()
        {
            Vector3 castPosition; // UNITIALIZED
            Vector3 nextBuffVars_CastPosition;
            Vector3 nextBuffVars_sourcePosition;
            Vector3 sourcePosition;
            int count;
            nextBuffVars_CastPosition = castPosition;
            FaceDirection(owner, castPosition);
            sourcePosition = GetPointByUnitFacingOffset(owner, -25, 0);
            nextBuffVars_sourcePosition = sourcePosition;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Heartseeker(nextBuffVars_CastPosition, nextBuffVars_sourcePosition), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_HeartseekerSound(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_HeartseekerChannel(), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
                {
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
        public override void ChannelingStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerChannel), (ObjAIBase)owner, 0);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_Heartseeker), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerSound), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerChannel), (ObjAIBase)owner, 0);
        }
    }
}