#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Chilled : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Freeze.troy", },
            BuffName = "Chilled",
            BuffTextureName = "3022_Frozen_Heart.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        float attackSpeedMod;
        public Chilled(float movementSpeedMod = default, float attackSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
            //RequireVar(this.attackSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}