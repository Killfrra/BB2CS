#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CassiopeiaSecondSkin : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", },
            AutoBuffActivateEffect = new[]{ "Judicator_buf.troy", },
            BuffName = "CassiopeiaSecondSkin",
            BuffTextureName = "Cassiopeia_DeadlyCadence.dds",
            NonDispellable = true,
        };
        float testAmount;
        public override void OnActivate()
        {
            this.testAmount = charVars.SecondSkin;
            SetBuffToolTipVar(1, charVars.SecondSkin);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, charVars.SecondSkin);
            IncFlatSpellBlockMod(owner, charVars.SecondSkin);
        }
        public override void OnUpdateActions()
        {
            float healthPercent;
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(healthPercent > 0.5f)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                if(this.testAmount != charVars.SecondSkinMR)
                {
                    this.testAmount = charVars.SecondSkin;
                    SetBuffToolTipVar(1, charVars.SecondSkin);
                }
            }
        }
    }
}