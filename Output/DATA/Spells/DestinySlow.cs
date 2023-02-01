﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DestinySlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", },
            BuffName = "Destiny Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        float attackSpeedMod;
        public DestinySlow(float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}