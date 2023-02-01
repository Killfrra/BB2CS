#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class StrengthOfSpirit : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float multiplier;
        float hpRegen;
        public StrengthOfSpirit(float multiplier = default)
        {
            this.multiplier = multiplier;
        }
        public override void OnActivate()
        {
            this.hpRegen = 0;
            //RequireVar(this.multiplier);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, this.hpRegen);
        }
        public override void OnUpdateActions()
        {
            float maxMana;
            maxMana = GetMaxPAR(target, PrimaryAbilityResourceType.MANA);
            this.hpRegen = this.multiplier * maxMana;
        }
    }
}