#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KillerInstinctGain : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float incrementGain;
        public KillerInstinctGain(float incrementGain = default)
        {
            this.incrementGain = incrementGain;
        }
        public override void OnActivate()
        {
            //RequireVar(this.incrementGain);
            charVars.IncrementGain += this.incrementGain;
        }
    }
}