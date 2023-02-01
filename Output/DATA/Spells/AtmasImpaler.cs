#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AtmasImpaler : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Atma's Impaler",
            BuffTextureName = "3005_Atmas_Impaler.dds",
        };
        float ownerHealth;
        public override void OnActivate()
        {
            this.ownerHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            float lessOwnerHealth;
            lessOwnerHealth = this.ownerHealth * 0.02f;
            IncFlatPhysicalDamageMod(owner, lessOwnerHealth);
        }
        public override void OnUpdateActions()
        {
            this.ownerHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}