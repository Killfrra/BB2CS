﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawLivingArtilleryCost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KogMawLivingArtillery",
            BuffTextureName = "KogMaw_LivingArtillery.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
    }
}