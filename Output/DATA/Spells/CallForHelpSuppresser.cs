﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CallForHelpSuppresser : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "CallforHelpSuppresser",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        public override void OnActivate()
        {
            SetCallForHelpSuppresser(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCallForHelpSuppresser(owner, false);
        }
    }
}