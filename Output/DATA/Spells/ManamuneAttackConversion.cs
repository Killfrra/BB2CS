#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ManamuneAttackConversion : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Atma's Impaler",
            BuffTextureName = "3005_Atmas_Impaler.dds",
        };
        float ownerMana;
        public override void OnActivate()
        {
            this.ownerMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            float lessOwnerMana;
            lessOwnerMana = this.ownerMana * 0.02f;
            IncFlatPhysicalDamageMod(owner, lessOwnerMana);
        }
        public override void OnUpdateActions()
        {
            this.ownerMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}