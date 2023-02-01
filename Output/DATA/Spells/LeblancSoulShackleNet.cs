#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancSoulShackleNet : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "leBlanc_shackle_tar.troy", "leBlanc_shackle_tar_blood.troy", },
            BuffName = "LeblancShackle",
            BuffTextureName = "LeblancConjureChains.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}