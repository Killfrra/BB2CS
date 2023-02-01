#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CardMasterStackHolder : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "CardMasterStack",
            BuffTextureName = "Cardmaster_RapidToss_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.CardMasterStackHolder));
            if(count >= 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CardmasterStackParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.CardMasterStackHolder), 0);
            }
        }
    }
}