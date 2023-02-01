#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeathDefiedSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Death Defied",
            BuffTextureName = "Lich_Untransmutable.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            if(!owner.IsDead)
            {
                ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)owner);
            }
        }
    }
}