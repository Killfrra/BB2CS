#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonSliceAndDiceDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RenektonSliceAndDiceShred",
            BuffTextureName = "Renekton_Dice.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float armorShred;
        public RenektonSliceAndDiceDebuff(float armorShred = default)
        {
            this.armorShred = armorShred;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorShred);
            IncPercentArmorMod(owner, this.armorShred);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, this.armorShred);
        }
    }
}