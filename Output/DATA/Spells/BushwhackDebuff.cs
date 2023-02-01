#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BushwhackDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Bushwhack",
            BuffTextureName = "NIdalee_Bushwhack.dds",
        };
        float debuff;
        public BushwhackDebuff(float debuff = default)
        {
            this.debuff = debuff;
        }
        public override void OnActivate()
        {
            float tooltipDebuff;
            ApplyAssistMarker(attacker, owner, 10);
            IncPercentArmorMod(owner, this.debuff);
            IncPercentSpellBlockMod(owner, this.debuff);
            tooltipDebuff = this.debuff * -100;
            SetBuffToolTipVar(1, tooltipDebuff);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, this.debuff);
            IncPercentSpellBlockMod(owner, this.debuff);
        }
    }
}