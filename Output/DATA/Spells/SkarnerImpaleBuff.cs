#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SkarnerImpaleBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SkarnerImpaleBuff",
            BuffTextureName = "SkarnerImpale.dds",
        };
        public override void OnDeath()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            SpellBuffClear(caster, nameof(Buffs.SkarnerImpale));
        }
    }
}