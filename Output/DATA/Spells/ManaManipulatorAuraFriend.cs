#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ManaManipulatorAuraFriend : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Mana Regeneration Aura",
            BuffTextureName = "3037_Mana_Manipulator.dds",
        };
        float manaRegenBonus;
        public ManaManipulatorAuraFriend(float manaRegenBonus = default)
        {
            this.manaRegenBonus = manaRegenBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaRegenBonus);
        }
        public override void OnUpdateStats()
        {
            if(owner is Champion)
            {
                IncFlatPARRegenMod(owner, this.manaRegenBonus, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}