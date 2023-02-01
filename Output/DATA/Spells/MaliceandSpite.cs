﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaliceandSpite : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", "", },
            AutoBuffActivateEffect = new[]{ "evelyn_maliceSpite_buf.troy", "evelyn_maliceSpite_buf.troy", "evelyn_maliceSpite_speed_buf.troy", },
            BuffName = "Malice And Spite",
            BuffTextureName = "Evelynn_Drink.dds",
        };
        float[] effect0 = {0.2f, 0.25f, 0.3f};
        float[] effect1 = {0.25f, 0.5f, 0.75f};
        public override void OnActivate()
        {
            PlayAnimation("Spell4", 0, owner, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.EvelynnUnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncPercentMovementSpeedMod(owner, this.effect0[level]);
            IncPercentAttackSpeedMod(owner, this.effect1[level]);
        }
    }
}
namespace Spells
{
    public class MaliceandSpite : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(attacker, target, new Buffs.MaliceandSpite(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}