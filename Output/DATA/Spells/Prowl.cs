﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Prowl : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", },
            AutoBuffActivateEffect = new[]{ "nidalee_prowl_buf.troy", },
            BuffName = "Prowl",
            BuffTextureName = "Nidalee_OnTheProwl.dds",
        };
        public override void OnActivate()
        {
            IncPercentMovementSpeedMod(owner, 0.15f);
            OverrideAnimation("Run", "Run2", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.15f);
        }
    }
}