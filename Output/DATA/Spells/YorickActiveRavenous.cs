#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickActiveRavenous : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_buffbone_glb_hand_loc", "r_buffbone_glb_hand_loc", "c_buffbone_glb_center_loc", },
            AutoBuffActivateEffect = new[]{ "yorick_ravenousGhoul_self_buf.troy", "yorick_ravenousGhoul_self_buf.troy", "yorick_ravenousGhoul_self_buf_spirits.troy", },
            BuffTextureName = "YorickRavenousPH.dds",
        };
        float lifestealPercent;
        public YorickActiveRavenous(float lifestealPercent = default)
        {
            this.lifestealPercent = lifestealPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifestealPercent);
            IncPercentLifeStealMod(owner, this.lifestealPercent);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, this.lifestealPercent);
        }
    }
}