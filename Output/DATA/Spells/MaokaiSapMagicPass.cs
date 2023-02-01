#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiSapMagicPass : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiSapMagicPass",
            BuffTextureName = "MaokaiSapMagic.dds",
            PersistsThroughDeath = true,
        };
        float healAmount;
        public override void OnActivate()
        {
            this.healAmount = 0;
        }
        public override void OnUpdateActions()
        {
            float maxHP;
            maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            this.healAmount = maxHP * 0.07f;
            this.healAmount = MathF.Floor(this.healAmount);
            SetBuffToolTipVar(1, this.healAmount);
        }
    }
}