#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WeaponMastery : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WeaponMastery",
            BuffTextureName = "Armsmaster_MasterOfArms.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float damageAdded;
        float weaponDamage;
        public override void OnActivate()
        {
            //RequireVar(this.damageAdded);
            this.weaponDamage = GetFlatPhysicalDamageMod(owner);
        }
        public override void OnUpdateStats()
        {
            this.damageAdded = this.weaponDamage * 0.131f;
            IncFlatPhysicalDamageMod(owner, this.damageAdded);
        }
        public override void OnUpdateActions()
        {
            this.weaponDamage = GetFlatPhysicalDamageMod(owner);
        }
    }
}