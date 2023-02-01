#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasHappyHour : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasHappyHour",
            BuffTextureName = "GragasPassiveHeal.dds",
        };
        float healAmount;
        public override void OnUpdateStats()
        {
            float maxHP;
            float healHP;
            maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            healHP = maxHP * 0.02f;
            this.healAmount = MathF.Floor(healHP);
            SetBuffToolTipVar(1, this.healAmount);
        }
    }
}