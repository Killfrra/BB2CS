#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Turret Shield",
            BuffTextureName = "035_Tower_Shield.dds",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float gameTime;
            float aoeReduction;
            gameTime = GetGameTime();
            aoeReduction = gameTime * 0.000111f;
            aoeReduction = Math.Min(aoeReduction, 0.2f);
            aoeReduction = Math.Max(aoeReduction, 0);
            aoeReduction *= 100;
            SetBuffToolTipVar(1, aoeReduction);
        }
        public override void OnUpdateActions()
        {
            float gameTime;
            float aoeReduction;
            if(ExecutePeriodically(30, ref this.lastTimeExecuted, false))
            {
                gameTime = GetGameTime();
                aoeReduction = gameTime * 0.000111f;
                aoeReduction = Math.Min(aoeReduction, 0.2f);
                aoeReduction = Math.Max(aoeReduction, 0);
                aoeReduction *= 100;
                SetBuffToolTipVar(1, aoeReduction);
            }
        }
    }
}