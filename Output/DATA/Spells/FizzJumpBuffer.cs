﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FizzJumpBuffer : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void SelfExecute()
        {
            Vector3 jumpBuffer;
            jumpBuffer = GetCastSpellTargetPos();
            charVars.JumpBuffer = jumpBuffer;
            AddBuff((ObjAIBase)owner, owner, new Buffs.FizzJumpBuffered(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class FizzJumpBuffer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzJumpBuffer));
            SetSlotSpellCooldownTimeVer2(0.15f, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}