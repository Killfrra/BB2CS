#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SiphoningStrikeDamageBonus : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CannibalismMaxHPGained",
            BuffTextureName = "Sion_SpiritFeast.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float damageBonus;
        public SiphoningStrikeDamageBonus(float damageBonus = default)
        {
            this.damageBonus = damageBonus;
        }
        public override void OnActivate()
        {
            charVars.DamageBonus += this.damageBonus;
        }
    }
}