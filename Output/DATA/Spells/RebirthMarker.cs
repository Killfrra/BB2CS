#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RebirthMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rebirth Marker",
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int[] effect0 = {-40, -40, -40, -40, -25, -25, -25, -10, -10, -10, -10, 5, 5, 5, 20, 20, 20, 20};
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, -40);
        }
        public override void OnLevelUp()
        {
            int level;
            float rebirthArmorMod;
            level = GetLevel(owner);
            rebirthArmorMod = this.effect0[level];
            SetBuffToolTipVar(1, rebirthArmorMod);
        }
    }
}