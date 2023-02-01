#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EquipmentMastery : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EquipmentMastery",
            BuffTextureName = "Armsmaster_MasterOfArms.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float aPHealthAdded;
        float attackHealthAdded;
        float attackTotal;
        float aPTotal;
        public override void OnActivate()
        {
            //RequireVar(this.aPHealthAdded);
            //RequireVar(this.attackHealthAdded);
            this.attackTotal = GetFlatPhysicalDamageMod(owner);
            this.aPTotal = GetFlatMagicDamageMod(owner);
        }
        public override void OnUpdateStats()
        {
            this.aPHealthAdded = this.aPTotal * 2;
            this.attackHealthAdded = this.attackTotal * 3;
            IncMaxHealth(owner, this.aPHealthAdded, false);
            IncMaxHealth(owner, this.attackHealthAdded, false);
            SetBuffToolTipVar(1, this.attackHealthAdded);
            SetBuffToolTipVar(2, this.aPHealthAdded);
        }
        public override void OnUpdateActions()
        {
            this.aPTotal = GetFlatMagicDamageMod(owner);
            this.attackTotal = GetFlatPhysicalDamageMod(owner);
        }
    }
}