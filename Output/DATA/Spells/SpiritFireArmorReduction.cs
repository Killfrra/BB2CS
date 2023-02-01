#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SpiritFireArmorReduction : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SpiritFire",
            BuffTextureName = "Nasus_SpiritFire.dds",
        };
        Vector3 targetPos;
        float armorReduction;
        public SpiritFireArmorReduction(Vector3 targetPos = default, float armorReduction = default)
        {
            this.targetPos = targetPos;
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorReduction);
            //RequireVar(this.targetPos);
        }
        public override void OnUpdateStats()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float dist;
            IncFlatArmorMod(owner, this.armorReduction);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SpiritFireAoE)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                targetPos = this.targetPos;
                ownerPos = GetUnitPosition(owner);
                dist = DistanceBetweenPoints(targetPos, ownerPos);
                if(dist >= 450)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}