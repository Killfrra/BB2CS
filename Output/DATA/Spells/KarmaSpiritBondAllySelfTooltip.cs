﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KarmaSpiritBondAllySelfTooltip : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaSpiritBondAllySelf",
            BuffTextureName = "KarmaSpiritBond.dds",
        };
    }
}