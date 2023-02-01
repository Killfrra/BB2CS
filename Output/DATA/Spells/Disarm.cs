﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Disarm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "Disarm",
        };
        public override void OnActivate()
        {
            SetDisarmed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDisarmed(owner, false);
        }
    }
}