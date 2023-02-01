#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ExhaustDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "summoner_banish.troy", },
            BuffName = "ExhaustDebuff",
            BuffTextureName = "35.dds",
        };
        float armorMod;
        public ExhaustDebuff(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
            if(this.armorMod != 0)
            {
                ApplyAssistMarker(attacker, owner, 10);
            }
        }
        public override void OnUpdateStats()
        {
            if(this.armorMod != 0)
            {
                IncFlatArmorMod(owner, this.armorMod);
                IncFlatSpellBlockMod(owner, this.armorMod);
            }
        }
    }
}