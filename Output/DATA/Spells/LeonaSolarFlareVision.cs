﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaSolarFlareVision : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Region bubble;
        public LeonaSolarFlareVision(Region bubble = default)
        {
            this.bubble = bubble;
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubble);
        }
        public override void OnActivate()
        {
            //RequireVar(this.bubble);
        }
    }
}