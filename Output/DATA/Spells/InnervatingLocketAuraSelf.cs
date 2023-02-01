#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InnervatingLocketAuraSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ZettasManaManipulator_itm.troy", },
            BuffName = "InnervatingLocketAuraSelf",
            BuffTextureName = "3032_Innervating_Locket.dds",
        };
        float manaRegenBonus;
        float healthRegenBonus;
        public InnervatingLocketAuraSelf(float manaRegenBonus = default, float healthRegenBonus = default)
        {
            this.manaRegenBonus = manaRegenBonus;
            this.healthRegenBonus = healthRegenBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaRegenBonus);
            //RequireVar(this.healthRegenBonus);
        }
        public override void OnUpdateStats()
        {
            if(owner is Champion)
            {
                IncFlatPARRegenMod(owner, this.manaRegenBonus, PrimaryAbilityResourceType.MANA);
                IncFlatHPRegenMod(owner, this.healthRegenBonus);
                IncPercentCooldownMod(owner, -0.1f);
            }
        }
    }
}