#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeviathanAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeviathanAura",
            BuffTextureName = "3138_Leviathan.dds",
        };
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 0.85f;
        }
    }
}