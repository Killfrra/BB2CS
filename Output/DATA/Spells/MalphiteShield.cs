#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MalphiteShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ null, null, null, "", },
            BuffName = "MalphiteShield",
            BuffTextureName = "Malphite_GraniteShield.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float maxHP;
            float shieldHP;
            float shieldHealth;
            maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            shieldHP = maxHP * 0.1f;
            shieldHealth = MathF.Floor(shieldHP);
            SetBuffToolTipVar(1, shieldHealth);
        }
    }
}