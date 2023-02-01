#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TormentedSoilDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TormentedSoilDebuff",
            BuffTextureName = "FallenAngel_TormentedSoil.dds",
        };
        Vector3 targetPos;
        float mRminus;
        public TormentedSoilDebuff(Vector3 targetPos = default, float mRminus = default)
        {
            this.targetPos = targetPos;
            this.mRminus = mRminus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.mRminus);
        }
        public override void OnUpdateStats()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float dist;
            IncFlatSpellBlockMod(owner, this.mRminus);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.TormentedSoil)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                targetPos = this.targetPos;
                ownerPos = GetUnitPosition(owner);
                dist = DistanceBetweenPoints(targetPos, ownerPos);
                if(dist >= 308)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}