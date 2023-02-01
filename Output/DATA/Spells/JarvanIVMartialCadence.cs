#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVMartialCadence : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVMartialCadence",
            BuffTextureName = "JarvanIV_MartialCadence.dds",
            PersistsThroughDeath = true,
        };
        int[] effect0 = {6, 6, 6, 6, 6, 6, 8, 8, 8, 8, 8, 8, 10, 10, 10, 10, 10, 10};
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 6);
        }
        public override void OnLevelUp()
        {
            int level;
            float healthPerc;
            level = GetLevel(owner);
            healthPerc = this.effect0[level];
            SetBuffToolTipVar(1, healthPerc);
        }
    }
}