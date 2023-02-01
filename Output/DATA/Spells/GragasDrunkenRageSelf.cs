#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasDrunkenRageSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "GragasDrunkenRageSelf",
            BuffTextureName = "GragasDrunkenRage.dds",
        };
        float damageIncrease;
        float damageReduction;
        Particle arr;
        public GragasDrunkenRageSelf(float damageIncrease = default, float damageReduction = default)
        {
            this.damageIncrease = damageIncrease;
            this.damageReduction = damageReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageIncrease);
            //RequireVar(this.damageReduction);
            SpellEffectCreate(out this.arr, out _, "gragas_buff_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.arr);
        }
        public override void OnUpdateStats()
        {
            float damageReductionMod;
            IncPercentPhysicalReduction(owner, this.damageReduction);
            IncPercentMagicReduction(owner, this.damageReduction);
            IncFlatPhysicalDamageMod(owner, this.damageIncrease);
            damageReductionMod = 100 * this.damageReduction;
            SetBuffToolTipVar(1, this.damageIncrease);
            SetBuffToolTipVar(2, damageReductionMod);
        }
    }
}