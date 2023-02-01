#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PhysicalImmunity : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Invulnerability.troy", },
            BuffName = "PhysicalImmunity",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        public override void OnActivate()
        {
            SetPhysicalImmune(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetPhysicalImmune(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetPhysicalImmune(owner, true);
        }
    }
}