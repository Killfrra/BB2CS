#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InnervatingLocketAuraFriend : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ZettasManaManipulator_itm.troy", },
            BuffName = "InnervatingLocketAuraFriend",
            BuffTextureName = "3032_Innervating_Locket.dds",
        };
        float manaRegenBonus;
        float healthRegenBonus;
        public InnervatingLocketAuraFriend(float manaRegenBonus = default, float healthRegenBonus = default)
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
            }
        }
    }
}