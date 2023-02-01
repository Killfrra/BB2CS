#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShadowStepDodge : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShadowStepDodge",
            BuffTextureName = "Katarina_Shunpo.dds",
        };
        float damageReduction;
        Fade iD; // UNUSED
        public ShadowStepDodge(float damageReduction = default)
        {
            this.damageReduction = damageReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageReduction);
            this.iD = PushCharacterFade(owner, 0.5f, 0.1f);
        }
        public override void OnDeactivate(bool expired)
        {
            this.iD = PushCharacterFade(owner, 1, 0.5f);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, this.damageReduction);
            IncPercentMagicReduction(owner, this.damageReduction);
        }
    }
}