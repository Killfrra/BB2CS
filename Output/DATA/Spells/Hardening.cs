#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Hardening : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hardening",
            BuffTextureName = "GreenTerror_ChitinousExoplates.dds",
        };
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            float hardnessPercent;
            float percentReduction;
            hardnessPercent = GetPercentHardnessMod(owner);
            percentReduction = 1 - hardnessPercent;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.SNARE)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.SLOW)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.FEAR)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.CHARM)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.SLEEP)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.STUN)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.TAUNT)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.SILENCE)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.BLIND)
                {
                    duration *= percentReduction;
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
        public override void OnUpdateStats()
        {
            float percentReduction;
            percentReduction = GetPercentHardnessMod(owner);
            percentReduction *= 100;
            if(percentReduction >= 0)
            {
                SetBuffToolTipVar(1, percentReduction);
            }
            else
            {
                SetBuffToolTipVar(1, 0);
            }
        }
    }
}