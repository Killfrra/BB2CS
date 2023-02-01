#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenCommandOther : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GarenCommandOther",
            BuffTextureName = "38.dds",
        };
        float damageReduction;
        Particle particle;
        float totalArmorAmount;
        public GarenCommandOther(float damageReduction = default, float totalArmorAmount = default)
        {
            this.damageReduction = damageReduction;
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageReduction);
            IncPercentPhysicalReduction(owner, this.damageReduction);
            IncPercentMagicReduction(owner, this.damageReduction);
            SpellEffectCreate(out this.particle, out _, "garen_commandingPresence_unit_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SetBuffToolTipVar(1, this.totalArmorAmount);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, this.damageReduction);
            IncPercentMagicReduction(owner, this.damageReduction);
        }
    }
}