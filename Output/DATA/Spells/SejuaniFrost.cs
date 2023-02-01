#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniFrost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Sejuani_Frost.troy", },
            BuffName = "SejuaniFrost",
            BuffTextureName = "Sejuani_Frost.dds",
        };
        float movementSpeedMod;
        bool indicator;
        Particle overhead;
        public SejuaniFrost(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            this.indicator = false;
            if(owner is Champion)
            {
                this.indicator = true;
            }
            else if(owner is Clone)
            {
                this.indicator = true;
            }
            if(this.indicator)
            {
                SpellEffectCreate(out this.overhead, out _, "Sejuani_Frost_Overhead.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, "spine", default, attacker, "Bird_head", default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.indicator)
            {
                SpellEffectRemove(this.overhead);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
        }
    }
}