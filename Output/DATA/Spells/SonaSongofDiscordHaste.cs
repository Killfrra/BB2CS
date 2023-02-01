﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaSongofDiscordHaste : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Haste.troy", "SonaSongofDiscord_tarB.troy", },
            BuffName = "SonaSongofDiscordHaste",
            BuffTextureName = "Sona_SongofDiscord.dds",
        };
        float moveSpeedMod;
        public SonaSongofDiscordHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}