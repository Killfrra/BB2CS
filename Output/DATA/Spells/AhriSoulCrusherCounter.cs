#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriSoulCrusherCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AhriSoulCrusherCounter",
            BuffTextureName = "Ahri_SoulEater2.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AhriSoulCrusherCounter));
            if(count >= 9)
            {
                AddBuff(attacker, attacker, new Buffs.AhriSoulCrusher(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AhriSoulCrusherCounter), 0);
                SpellBuffRemove(owner, nameof(Buffs.AhriIdleParticle), (ObjAIBase)owner, 0);
            }
        }
    }
}