﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TwitchSprayAndPray : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Full Automatic",
            BuffTextureName = "Twitch_Clone.dds",
        };
    }
}