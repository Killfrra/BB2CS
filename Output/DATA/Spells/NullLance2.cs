﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NullLance2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Null_Lance_buf.troy", },
        };
        int passthrough;
        float spellSlowPercent;
        float lastTimeExecuted;
        public NullLance2(int passthrough = default, float spellSlowPercent = default)
        {
            this.passthrough = passthrough;
            this.spellSlowPercent = spellSlowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.passthrough);
            //RequireVar(this.spellSlowPercent);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.4f, ref this.lastTimeExecuted, false))
            {
                if(this.passthrough == 0)
                {
                    float spellCD1;
                    float spellCD1a;
                    spellCD1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    spellCD1a = spellCD1 * this.spellSlowPercent;
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, spellCD1a);
                    SpellEffectCreate(out _, out _, "ChronoRefresh_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                }
                if(this.passthrough == 1)
                {
                    float spellCD2;
                    float spellCD2a;
                    spellCD2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    spellCD2a = spellCD2 * this.spellSlowPercent;
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, spellCD2a);
                    SpellEffectCreate(out _, out _, "ChronoRefresh_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                }
                if(this.passthrough == 2)
                {
                    float spellCD3;
                    float spellCD3a;
                    spellCD3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    spellCD3a = spellCD3 * this.spellSlowPercent;
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, spellCD3a);
                    SpellEffectCreate(out _, out _, "ChronoRefresh_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                }
            }
        }
    }
}