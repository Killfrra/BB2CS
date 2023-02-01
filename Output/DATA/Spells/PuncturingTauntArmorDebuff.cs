#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PuncturingTauntArmorDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "PuncturingTaunt",
            BuffTextureName = "Armordillo_ScaledPlating.dds",
            PopupMessage = new[]{ "game_floatingtext_Taunted", },
        };
        float armorDebuff;
        public PuncturingTauntArmorDebuff(float armorDebuff = default)
        {
            this.armorDebuff = armorDebuff;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorDebuff);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.Taunt), attacker);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorDebuff);
        }
    }
}